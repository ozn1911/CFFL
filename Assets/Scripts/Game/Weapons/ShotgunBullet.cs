using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game.Weapons
{
    public class ShotgunBullet : Bullet
    {
        [SerializeField]
        GameObject altBullet;
        [SerializeField]
        int bulletAmount;
        [SerializeField]
        Transform[] Bullets;
        public float Spread;

        private void Awake()
        {
            Bullets = new Transform[bulletAmount];
            for(int i = 0; i< bulletAmount; i++)
            {
                GameObject bullet = Instantiate(altBullet);
                Bullets[i] = bullet.transform;
                Bullet script = bullet.GetComponent<Bullet>();
                script.Damage = Damage;
                script.Lifetime = Lifetime;
                script.Speed = Speed;
                script.GetDisabledOnHit = GetDisabledOnHit;
            }
        }
        private void OnEnable()
        {
            foreach(Transform transf in Bullets)
            {
                transf.position = transform.position;
                transf.rotation = transform.rotation;
                transf.Rotate(new Vector3(Random.Range(-Spread, Spread), Random.Range(-Spread, Spread), 0));
                Bullet script = transf.GetComponent<Bullet>();
                script.TrailClear();
                script.Damage = Damage;
                script.Lifetime = Lifetime;
                script.Speed = Speed;
                transf.gameObject.SetActive(true);
            }
        }
    }
}