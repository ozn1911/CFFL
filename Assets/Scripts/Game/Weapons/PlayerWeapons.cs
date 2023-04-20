using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Weapons
{
    public class PlayerWeapons : MonoBehaviour
    {
        private WeaponsEnum _currentWeapon;
        [SerializeField]
        private GameObject[] _weapons;
        private WeaponStructure[] _weaponStructures;

        Ray ray;
        RaycastHit hit;
        bool mousefire;



        private void Awake()
        {
            _weaponStructures = new WeaponStructure[_weapons.Length];
            for(int i = 0; i < _weapons.Length; i++)
            {
                _weaponStructures[i] = _weapons[i].GetComponent<WeaponStructure>();
            }
        }


        public WeaponsEnum CurrentWeapon;

        public void WeaponFire()
        {
            WeaponStructure structure = _weaponStructures[((int)_currentWeapon)];
            structure.Pool.FireBulletLookat(start: structure.Barrel.position,direction: hit.point, 4f);
        }

        private void Update()
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hitt = Physics.Raycast(ray, out hit, 500, LayerMask.GetMask("GunLayer"));
            _weapons[((int)_currentWeapon)].transform.LookAt( hit.point, Vector3.up);
            if(hitt && Input.GetMouseButton(0))
            {
                mousefire = true;
            }
            else
            {
                mousefire = false;
            }
        }

        private void FixedUpdate()
        {
            if(mousefire)
                WeaponFire();
        }


        public void ChangeWeapon(WeaponsEnum weapon)
        {
            CurrentWeapon = weapon;
        }


        



    }

}