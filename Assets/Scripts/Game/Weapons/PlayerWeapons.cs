using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.Weapons
{
    public class PlayerWeapons : MonoBehaviour
    {
        [SerializeField]
        private WeaponsEnum _currentWeapon;
        [SerializeField]
        private GameObject[] _weapons;
        public uint[] Ammonution;
        public uint[] AmmoClip;
        float lastfire;
        private int _reloadTicks = 0;
        private WeaponStructure[] _weaponStructures;


        Ray ray;
        RaycastHit hit;
        bool mousefire;

#nullable enable
        [SerializeField]
        Slider? reloadSlider;
        bool reloadSliderExistence;



        private void Awake()
        {
            ReloadSliderVibeCheck();
            ArraysOfWeapons();

            #region Functions
            void ReloadSliderVibeCheck()
            {
                if (reloadSlider != null)
                {
                    reloadSlider.gameObject.SetActive(false);
                    reloadSliderExistence = true;
                }
            }

            void ArraysOfWeapons()
            {
                _weaponStructures = new WeaponStructure[_weapons.Length];
                Ammonution = new uint[_weapons.Length];
                AmmoClip = new uint[_weapons.Length];
                for (int i = 0; i < _weapons.Length; i++)
                {
                    _weaponStructures[i] = _weapons[i].GetComponent<WeaponStructure>();
                }
            } 
            #endregion
        }



        private void Update()
        {
            bool hitt = KnowWhereToShoot();
            ConfirmWhereToShoot();
            KnowWhenToShoot(hitt);

            #region Functions
            bool KnowWhereToShoot()
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                bool hitt = Physics.Raycast(ray, out hit, 500, LayerMask.GetMask("GunLayer"));
                return hitt;
            }
            void ConfirmWhereToShoot()
            {
                _weapons[((int)_currentWeapon)].transform.LookAt(hit.point, Vector3.up);
            }
            void KnowWhenToShoot(bool hitt)
            {
                if (hitt && Input.GetMouseButton(0))
                {
                    mousefire = true;
                }
                else
                {
                    mousefire = false;
                }
                if (Input.mouseScrollDelta.y != 0)
                {
                    WeapSwitch(((int)Input.mouseScrollDelta.y));
                }
            } 
            #endregion
        }

        private void FixedUpdate()
        {
            ReloadUpdate();
            Weapon[] wpStats = WeaponStats.instance.Weapons;
            if (mousefire)
                if (lastfire + wpStats[((int)_currentWeapon)].FireRate < Time.time)
                {
                    if (AmmoClip[((int)_currentWeapon)] > 0)
                    {
                        lastfire = Time.time;
                        AmmoClip[((int)_currentWeapon)]--;
                        WeaponFire();
                    }
                    else
                    {
                        if (_reloadTicks <= 0)
                        {
                            StartReload(wpStats[((int)_currentWeapon)].ReloadTicks);
                        }
                    }
                }
        }





        #region Weapon things
        public void WeapSwitch(int swtch)
        {
            _reloadTicks = -1;
            _weapons[((int)_currentWeapon)].SetActive(false);
            Calculate(swtch);
            _weapons[((int)_currentWeapon)].SetActive(true);

            void Calculate(int swtch)
            {
                int temp = (((int)_currentWeapon) + swtch) % (_weapons.Length);
                if (temp < 0)
                {
                    temp += _weapons.Length;
                }
                _currentWeapon = (WeaponsEnum)temp;
            }
        }

        public void StartReload(int ticks)
        {
            if (ticks != 0)
            {
                if(reloadSliderExistence && Ammonution[((int)_currentWeapon)] != 0)
                {
                    
                    reloadSlider.value = 0;
                    reloadSlider.gameObject.SetActive(true);
                }
                _reloadTicks = ticks;
            }
            else
            {
                ReloadAmmoClip(((int)_currentWeapon));
            }
        }


        /// <summary>
        /// me coding at 3am. edit: its 4 now.
        /// </summary>
        public void ReloadUpdate()
        {
            if (_reloadTicks > 0)
            {
                _reloadTicks--;
                if (reloadSliderExistence)
                {
                    reloadSlider.value = (1f - ((float)(_reloadTicks) / (float)(WeaponStats.instance.Weapons[((int)_currentWeapon)].ReloadTicks)));
                    if (_reloadTicks == 0)
                    {
                        ReloadAmmoClip(((int)_currentWeapon));
                        reloadSlider.gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (_reloadTicks == 0)
                    {
                        ReloadAmmoClip(((int)_currentWeapon));
                    }
                }
            }
        }
        public void ReloadAmmoClip(int clip)
        {
            Weapon[] wpStats = WeaponStats.instance.Weapons;
            uint clipAmmo = wpStats[clip].ClipAmmo;
            uint preclipReq = clipAmmo - AmmoClip[clip];
            if (Ammonution[clip] >= clipAmmo)
            {
                Ammonution[clip] -= preclipReq;
                AmmoClip[clip] += preclipReq;
            }
            else
            {
                uint preMinus = Ammonution[clip];
                Ammonution[clip] = 0;
                AmmoClip[clip] += preMinus;
            }
        }


        public void WeaponFire()
        {
            WeaponStructure structure = _weaponStructures[((int)_currentWeapon)];
            Weapon wp = WeaponStats.instance.Weapons[((int)_currentWeapon)];
            structure.Pool.FireBulletLookat(start: structure.Barrel.position, direction: hit.point, wp.BulletDamage, wp.BulletSpeed, wp.BulletLifetime);
        }

        #endregion




    }

}