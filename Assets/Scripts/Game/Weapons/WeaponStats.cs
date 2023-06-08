using System.Collections;
using UnityEngine;




namespace Assets.Scripts.Game.Weapons
{
    [CreateAssetMenu(fileName = "WeaponStats", menuName = "Dat?/WeaponStats")]
    public class WeaponStats : ScriptableObject
    {
        public static WeaponStats instance;
        public Weapon[] Weapons;
        public void Initialize()
        {
            instance = this;
        }
        private void OnValidate()
        {
            for(int i = 0; i < Weapons.Length; i++)
            {
                Weapons[i].WeaponName = ((WeaponsEnum)i).ToString();
            }
        }


    }
    public enum WeaponsEnum
    {
        DebugWeap,
        pistol,
        smg,
        shotgun,
        flamethrower,
        minigun
    }
}