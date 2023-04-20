using UnityEngine;
using UnityEditor;

namespace Assets.Scripts.Game.Weapons
{
    public class BulletPool : MonoBehaviour
    {
        [Header("Pool Settings")]
        [SerializeField]
        public int PoolSize = 100;
        int _currentBullet = 0;
        [SerializeField, NonReorderable]
        GameObject[] _bulletPool;
        Bullet[] _bulletData;

        [SerializeField]
        GameObject _bulletTemplate;


        private void Awake()
        {
            _bulletPool = new GameObject[PoolSize];
            _bulletData = new Bullet[PoolSize];
            for (int i = 0; i < PoolSize; i++)
            {
                if ((_bulletPool[i] == null))
                {
                    GameObject bullet = Instantiate(_bulletTemplate);
                    _bulletData[i] = bullet.GetComponent<Bullet>();
                    bullet.SetActive(false);
                    _bulletPool[i] = bullet;
                }
            }
        }



        public void FireBullet(Vector3 start, Vector3 direction, float bulletLifetime)
        {
            GameObject bullet = GetBullet(out int index);
            _bulletData[index].Lifetime = bulletLifetime;
            bullet.transform.position = start;
            bullet.transform.LookAt(bullet.transform.position + direction);
            bullet.SetActive(true);
        }
        public void FireBulletLookat(Vector3 start, Vector3 direction, float bulletLifetime)
        {
            GameObject bullet = GetBullet(out int index);
            _bulletData[index].Lifetime = bulletLifetime;
            bullet.transform.position = start;
            bullet.transform.LookAt(direction, Vector3.up);
            bullet.SetActive(true);
        }
        GameObject GetBullet(out int index)
        {
            int i = 0;
            while (_bulletPool[(_currentBullet + i)%(PoolSize)].activeSelf)
            {
                i++;
                if (i == PoolSize)
                {
                    Debug.LogError("_bulletPool is all used up, you need to incerase bullet pool");
                    goto escape;
                    while (true ? false ? true : false : true ? false : true || false ? true ? false : true : false ? true : false)
                        Debug.Log("why so serious?");
                }
            }
            _currentBullet += i;
            _currentBullet %= PoolSize;
            index = _currentBullet;
            return _bulletPool[_currentBullet];
        escape:
            index = 0;
            return null; //this will probably crash the game, in case logerror didnt already stopped it

        }

    }
}