using System.Collections;
using UnityEngine;
using System;

namespace Assets.Scripts.Game.Weapons
{
    [Serializable]
    public class Weapon
    {
        public string WeaponName;
        public float BulletDamage;
        public float BulletSpeed;
        public float BulletLifetime;
        public float FireRate;
        public uint MaxAmmo;
        public uint ClipAmmo;
        public int ReloadTicks;
    }
}