using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Game.Drops
{
    public class LootSpin : MonoBehaviour
    {
        [SerializeField]
        Rigidbody _rb;
        [SerializeField]
        Transform _sub;
        private void FixedUpdate()
        {
            _rb.MoveRotation(Quaternion.Euler(0,Time.time * 75,0));
            _sub.localPosition = new Vector3(0, (Mathf.Sin(Time.time) / 2) + 0.5f,0);
        }
    }
}