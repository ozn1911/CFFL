using System.Collections;
using UnityEngine;
using System;

namespace Assets.Scripts.Game.Weapons
{
    [Serializable]
    public class Bullet : MonoBehaviour
    {
        public float Speed;
        public float Damage;
        public float Lifetime;

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
        //private void OnCollisionEnter(Collision collision)
        //{
        //    collision.gameObject.SendMessage("TakeDamage", Damage, SendMessageOptions.DontRequireReceiver);
        //    gameObject.SetActive(false);
        //}
        private void OnTriggerEnter(Collider other)
        {
            gameObject.SetActive(false);
            other.gameObject.SendMessage("TakeDamage", Damage, SendMessageOptions.DontRequireReceiver);
        }

    }
}