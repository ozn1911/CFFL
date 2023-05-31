using System.Collections;
using UnityEngine;
using Assets.Scripts.Game.Weapons;

namespace Assets.Scripts.Game.Drops
{
    public class BulletBox : MonoBehaviour
    {
        public WeaponsEnum Tagret;
        public uint amount;


        private void OnTriggerEnter(Collider other)
        {
            if(other.tag.Equals("Player"))
            {
                PlayerWeapons.s_GetAmmo pack;
                pack.weapon = Tagret;
                pack.amount = amount;
                other.SendMessage("GetAmmo", pack, SendMessageOptions.DontRequireReceiver);
                transform.parent.gameObject.SetActive(false);
            }
        }
    }
}