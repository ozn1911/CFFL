using System.Collections;
using UnityEngine;
using System;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.Weapons
{
    [Serializable]
    public class Bullet : MonoBehaviour
    {
        public float Speed;
        public float Damage;
        public float Lifetime;
        public bool GetDisabledOnHit = true;
#nullable enable
        public TrailRenderer? trail;

        public void SetStats(float speed, float damage)
        {
            Speed = speed;
            Damage = damage;
        }
        

        private void FixedUpdate()
        {
            transform.position += transform.forward * Speed;
            if (Lifetime > 0)
                Lifetime -= Time.fixedDeltaTime;
            else
                gameObject.SetActive(false);
        }
        private void OnTriggerEnter(Collider other)
        {
            other.gameObject.SendMessage("TakeDamage", Damage, SendMessageOptions.DontRequireReceiver);
            if(GetDisabledOnHit)
                gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            if(trail != null)
                trail.Clear();
        }
    }
}