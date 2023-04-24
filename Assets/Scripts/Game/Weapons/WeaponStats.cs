using System.Collections;
using UnityEngine;




namespace Assets.Scripts.Game.Weapons
{
    [CreateAssetMenu(fileName = "WeaponStats", menuName = "Dat?/WeaponStats")]
    public class WeaponStats : ScriptableObject
    {
        public static WeaponStats instance;
        public Weapon[] Weapons;
        private void OnEnable()
        {
            instance = this;
        }


    }
    public enum WeaponsEnum
    {
        DebugWeap,
        pistol
    }
}