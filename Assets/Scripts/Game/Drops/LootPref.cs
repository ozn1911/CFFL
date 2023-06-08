using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game.Drops
{
    [CreateAssetMenu(fileName = "LootPref", menuName = "Dat?/LootPref")]
    public class LootPref : ScriptableObject
    {
        public static LootPref instance;
        public GameObject BulletBox;
        public GameObject HealthPack;

        public void Initialize()
        {
            instance = this;
        }
    }
}