using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System;
using Assets.Scripts.Game.Weapons;

namespace Assets.Scripts.Game.EnemySys.EnemyBehaviors
{
    public class EnemyScorpion : MonoBehaviour
    {
        public float MovementSpeed = 5f;

        [SerializeField]
        private WeaponStructure _weap;
        public float WeaponRange = 25f;


        private Rigidbody _rb;
        private Transform _player;
        private NavMeshPath _navPath;
        Action CurrentAcion;
        Vector3 _moveDir;
        bool _firing;
        float _lastTimeFired;
        [SerializeField]
        float _fireCD;

        private Animator animator; //AnimChanged
   

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _rb = GetComponent<Rigidbody>();
            CurrentAcion = Move;
            _navPath = new NavMeshPath();
            animator = this.gameObject.GetComponent<Animator>(); //AnimChanged
        }

        private void Update()
        {
            CurrentAcion();
        }
        private void FixedUpdate()
        {
            _moveDir.y = _rb.velocity.y;
            _rb.velocity = _moveDir;
            if (_lastTimeFired + _fireCD < Time.time && CurrentAcion == Firing)
            {
                _lastTimeFired = Time.time;
                animator.SetTrigger("NewakrepAttackAnim"); //AnimChanged 
                FireBullet();
            }
        }

        private void Move()
        {
            NavMesh.CalculatePath(transform.position, _player.position, NavMesh.AllAreas, _navPath);

            if (_navPath.status == NavMeshPathStatus.PathComplete && _navPath.corners.Length > 0)
            {
                int cornerIndex = 0;

                // Find the first reachable corner along the path
                for (int i = 0; i < _navPath.corners.Length; i++)
                {
                    if (Vector3.Distance(transform.position, _navPath.corners[i]) > 3f)
                    {
                        cornerIndex = i;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

                // Set the target position based on the reachable corner
                Vector3 targetPosition = _navPath.corners[cornerIndex];
                Vector3 direction = Vector3.Normalize(targetPosition - transform.position);


                _moveDir = direction * MovementSpeed;
                _moveDir.y = 0;
                transform.rotation = Quaternion.LookRotation(_moveDir, Vector3.up);
            }

            if(Vector3.Distance (transform.position, _player.position) < WeaponRange)
            {
                _moveDir = Vector3.zero;
                CurrentAcion = Firing;
            }
        }

        private void Firing()
        {
            if (Vector3.Distance(transform.position, _player.position) > WeaponRange)
            {
                _firing = false;
                CurrentAcion = Move;
            }
            Vector3 lookTag = (_player.position - transform.position);
            lookTag.y = 0;
            transform.rotation = Quaternion.LookRotation(lookTag, Vector3.up);
        }
        private void FireBullet()
        {
            _weap.Pool.FireBulletLookat(start: _weap.Barrel.position, direction: _player.position, damage: 5, speed: 0.5f, bulletLifetime: 2);
        }


        void OnDrawGizmos()
        {
            if (_navPath != null && _navPath.corners.Length > 0)
            {
                // Draw the path
                for (int i = 1; i < _navPath.corners.Length; i++)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(_navPath.corners[i - 1], _navPath.corners[i]);
                }

                // Draw the corners
                foreach (Vector3 corner in _navPath.corners)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawSphere(corner, 0.1f);
                }
            }
        }
    }
}