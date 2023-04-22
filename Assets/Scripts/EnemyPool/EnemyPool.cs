using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EnemyPool
{
    public class EnemyPool : MonoBehaviour
    {
        [Header("Pool Settings")]
        [SerializeField]
        public int PoolSize = 100;
        int _current = 0;
        [SerializeField, NonReorderable]
        GameObject[] _enemyPool;

        [SerializeField]
        GameObject _enemyTemplate;


        private void Awake()
        {
            _enemyPool = new GameObject[PoolSize];
            for (int i = 0; i < PoolSize; i++)
            {
                if ((_enemyPool[i] == null))
                {
                    GameObject bullet = Instantiate(_enemyTemplate);
                    bullet.SetActive(false);
                    _enemyPool[i] = bullet;
                }
            }
        }


        GameObject GetEnemy(out int index)
        {
            int i = 0;
            while (_enemyPool[(_current + i) % (PoolSize)].activeSelf)
            {
                i++;
                if (i == PoolSize)
                {
                    Debug.LogError("_enemyPool is all used up, you need to incerase bullet pool");
                    goto escape;
                }
            }
            _current += i;
            _current %= PoolSize;
            index = _current;
            return _enemyPool[_current];
        escape:
            index = 0;
            return null; //this will probably crash the game, in case logerror didnt already stopped it

        }





    }
}