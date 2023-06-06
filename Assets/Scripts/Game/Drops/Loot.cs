using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Assets.Scripts.Game.Drops
{
    public class Loot : MonoBehaviour
    {
        public void EnemyDeath()
        {
            GenLoot();
        }

        private void GenLoot()
        {
            int temp = UnityEngine.Random.Range(0, 100);

            //will rework this when this survives demo
            if(temp < 80)
            {
                GameObject foo = Instantiate(LootPref.instance.BulletBox, position: transform.position, Quaternion.identity);
                foo.GetComponentInChildren<BulletBox>().setLootRand();
            }
            else
            {
                GameObject foo = Instantiate(LootPref.instance.HealthPack, position: transform.position, Quaternion.identity);
                foo.GetComponentInChildren<HealthPack>().setLootRand();
            }
        }
    }
}