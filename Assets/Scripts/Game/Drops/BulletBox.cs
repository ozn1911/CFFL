using System.Collections;
using UnityEngine;
using Assets.Scripts.Game.Weapons;

namespace Assets.Scripts.Game.Drops
{
    public class BulletBox : MonoBehaviour
    {
        WeaponsEnum Tagret;
        uint amount;


        private void OnTriggerEnter(Collider other)
        {
            if(other.tag.Equals("Player"))
            {
                
            }
        }
    }
}