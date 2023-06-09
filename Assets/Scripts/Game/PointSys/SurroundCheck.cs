using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.PointSys
{
    public class SurroundCheck : MonoBehaviour
    {
        public float DetectionRadius = 10f;
        [SerializeField]
        private float _angle;
        public float Angle { get => _angle; }
        public float GetAngle { get => GetMaxEmptyAngle(); }
        private List<float> _enemyAngles = new List<float>();



        public float GetMaxEmptyAngle()
        {
            _enemyAngles.Clear();
            Collider[] enemies = Physics.OverlapSphere(transform.position, DetectionRadius);

            foreach(Collider coll in enemies)
            {
                if (coll.tag.Equals("Enemy"))
                {
                    Vector3 directionToEnemy = coll.transform.position - transform.position;
                    _enemyAngles.Add(Vector3.SignedAngle(transform.forward, directionToEnemy, Vector3.up) + 180);
                }
            }
            _enemyAngles.Sort();

            if (_enemyAngles.Count > 1)
            {
                float temp = _enemyAngles[0] + 360;
                float temp2 = _enemyAngles[_enemyAngles.Count - 1] - 360;
                _enemyAngles.Insert(0, temp2);
                _enemyAngles.Add(temp);

                float result = 0;
                for (int i = 0; i < _enemyAngles.Count - 1; i++)
                {
                    float foo = - _enemyAngles[i] + _enemyAngles[i+1];
                    if (result < foo)
                    {
                        result = foo;
                    }
                }
                _angle = result;
            }
            else
            {
                _angle = 360;
            }
            return _angle;
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, DetectionRadius);
        }
    }
}