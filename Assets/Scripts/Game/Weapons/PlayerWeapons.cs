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
        [SerializeField]
        private List<int> _ignoreWeapons;
        public uint[] Ammonution;
        private uint[] _ammoClip;
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
                _ammoClip = new uint[_weapons.Length];


                for (int i = 0; i < _weapons.Length; i++)
                {
                    _weaponStructures[i] = _weapons[i].GetComponent<WeaponStructure>();
                    if(Ammonution[i] == 0 && _ammoClip[i] == 0)
                    {
                        //_ignoreWeapons.Add(i);
                    }
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
                    if (_ammoClip[((int)_currentWeapon)] > 0)
                    {
                        lastfire = Time.time;
                        _ammoClip[((int)_currentWeapon)]--;
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
            if (reloadSliderExistence)
                reloadSlider.gameObject.SetActive(false);


            _reloadTicks = -1;
            _weapons[((int)_currentWeapon)].SetActive(false);
            Calculate(swtch);
            _weapons[((int)_currentWeapon)].SetActive(true);
            if(_ignoreWeapons.Contains(((int)_currentWeapon)))
            {
                
                WeapSwitch(swtch > 0 ? 1 : -1);
            }

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
            uint preclipReq = clipAmmo - _ammoClip[clip];
            if (Ammonution[clip] >= clipAmmo)
            {
                Ammonution[clip] -= preclipReq;
                _ammoClip[clip] += preclipReq;
            }
            else
            {
                if(Ammonution[clip] == 0)
                {
                    //_ignoreWeapons.Add(clip);
                }
                uint preMinus = Ammonution[clip];
                Ammonution[clip] = 0;
                _ammoClip[clip] += preMinus;
            }
        }


        public void WeaponFire()
        {
            WeaponStructure structure = _weaponStructures[((int)_currentWeapon)];
            Weapon wp = WeaponStats.instance.Weapons[((int)_currentWeapon)];
            structure.Pool.FireBulletLookat(start: structure.Barrel.position, direction: hit.point, wp.BulletDamage, wp.BulletSpeed, wp.BulletLifetime);
        }

        #endregion

        public void GetAmmo(s_GetAmmo GetAmmoInput)
        {
            int temp = ((int)GetAmmoInput.weapon);
            Ammonution[temp] += GetAmmoInput.amount;
            if(Ammonution[temp] > WeaponStats.instance.Weapons[temp].MaxAmmo)
            {
                Ammonution[temp] = WeaponStats.instance.Weapons[temp].MaxAmmo;
            }

            _ignoreWeapons.Remove(temp);
        }
        public struct s_GetAmmo
        {
            public WeaponsEnum weapon;
            public uint amount;
        }


    }

}