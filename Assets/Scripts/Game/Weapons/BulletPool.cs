using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Weapons
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField]
        public int PoolSize = 100;
        [SerializeField]
        GameObject _bulletTemplate;
        GameObject[] _bulletPool;
        int _currentBullet = 0;

        private void Awake()
        {
            _bulletPool = new GameObject[PoolSize];
            for (int i = 0; i < PoolSize; i++)
            {
                GameObject bullet = Instantiate(_bulletTemplate);
                bullet.SetActive(false);
                _bulletPool[i] = _bulletTemplate;
            }
        }
        




        GameObject GetBullet()
        {
            int i = 0;
            while(_bulletPool[_currentBullet + i].activeSelf)
            {
                i++;
                if(i == PoolSize)
                {
                    Debug.LogError("_bulletPool is all used up, you need to incerase bullet pool");
                    goto escape;
                    while (true? false? true : false : true? false : true || false? true? false : true : false? true : false)
                        Debug.Log("why so serious?");
                }
            }
            _currentBullet += i;
            return _bulletPool[_currentBullet];
            escape:
            return null; //this will crash the code, in case logerror didnt already stopped it

        }

    }
}