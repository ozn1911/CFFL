using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game.Drops
{
    public class HealthPack : MonoBehaviour
    {
        public int amount;


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                other.SendMessage("HealDamage", amount, SendMessageOptions.DontRequireReceiver);
                transform.parent.gameObject.SetActive(false);
            }
        }

        public void setLootRand()
        {
            amount = Random.Range(10,30);
        }
    }
}