using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.SceneData;

namespace Assets.Scripts.Game.EnemySys
{
    public class EnemyPool : MonoBehaviour
    {
        [Header("Pool Settings")]
        [SerializeField]
        public int PoolSize = 100;
        int _current = 0;

        [SerializeField]
        Transform _positioner1;
        [SerializeField]
        Transform _positioner2;



        [SerializeField, NonReorderable]
        GameObject[] _enemyPool;//not in use anymore but it still here so i can see if i messed it up
        [SerializeField]
        List<GameObject> enemyPool;
        //TODO: change array to list that supports waves, add count to living enemies, add event in case when enemies ends;
        [SerializeField]
        GameObject _enemyTemplate;



        //public static EnemyPool CreatePool0(GameObject go, int poolSize, Enemy enemy)
        //{
        //    EnemyPool temp = go.AddComponent<EnemyPool>();
        //    temp._enemyTemplate = EnemyData.instance.Enemies[((int)enemy)].Pref;
        //    temp.PoolSize = poolSize;
        //    temp._enemyPool = new GameObject[poolSize];
        //    iAmSlowlyExperiencingDepression(poolSize, temp);
        //    return temp;
        //
        //    static void iAmSlowlyExperiencingDepression(int poolSize, EnemyPool temp)//its almost 4 am now, i am slowly losing it ヽ(✿ﾟ▽ﾟ)ノ
        //    {
        //        for (int i = 0; i < poolSize; i++)
        //        {
        //            if ((temp._enemyPool[i] == null))//this wont work, its not nullable, idk, i was going to abaddon this code anyway
        //            {
        //                GameObject Enemy = Instantiate(temp._enemyTemplate);
        //                Enemy.SetActive(false);
        //                temp._enemyPool[i] = Enemy;
        //            }
        //        }
        //    }
        //}


        public void AssignPositioners(Transform pos1, Transform pos2)
        {
            _positioner1 = pos1;
            _positioner2 = pos2;
        }

        public static EnemyPool CreatePool(GameObject go, Wave wave)
        {
            EnemyPool temp = go.AddComponent<EnemyPool>();

            foreach(s_EnemyWave enemyStruct in wave.Enemies)
            {
                for(int i = 0; i < enemyStruct.Count; i++)
                {
                    GameObject Enemy = Instantiate(EnemyData.instance.Enemies[((int)enemyStruct.Enemy)].Pref, go.transform);
                    Enemy.SetActive(false);
                    temp.enemyPool.Add(Enemy);
                }
            }

            return temp;
        }


        GameObject GetEnemy(out int index)
        {
            int i = 0;
            while (enemyPool[(_current + i) % (enemyPool.Count)].activeSelf)
            {
                i++;
                if (i == enemyPool.Count)
                {
                    index = 0;
                    return null;
                }
            }
            _current += i;
            _current %= enemyPool.Count;
            index = _current;
            return enemyPool[_current];

        }

        public GameObject ActivateEnemy()
        {
            GameObject temp = GetEnemy(out int i);
            Vector3 dir = Vector3.Normalize(_positioner2.position -_positioner1.position);
            Vector3 pos = _positioner1.position + dir * Random.Range(0, Vector3.Distance(_positioner1.position, _positioner2.position));
            temp.transform.position = pos;
            temp.SetActive(true);
            return temp;
        }

        private void OnDestroy()
        {
            foreach(GameObject ga in enemyPool)
            {
                Destroy(ga);
            }
        }


    }
}