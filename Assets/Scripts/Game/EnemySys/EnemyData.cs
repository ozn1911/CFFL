using System.Collections;
using System;
using UnityEngine;

namespace Assets.Scripts.Game.EnemySys
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Dat?/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public static EnemyData instance;
        private void OnEnable()
        {
            instance = this;
        }
        public EnemyDataStruct[] Enemies;
#if UNITY_EDITOR
        private void OnValidate()
        {
            for(int i = 0; i < Enemies.Length; i++)
            {
                Enemies[i].Name = ((Enemy)i).ToString();
            }
        }
#endif


    }
    [Serializable]
    public struct EnemyDataStruct
    {
        public string Name;
        public GameObject Pref;
    }
    public enum Enemy
    {
        cube,
        golem,
        scorpion
    }
}