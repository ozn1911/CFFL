using UnityEngine;
using UnityEngine.AI;
using System;





namespace Assets.Scripts.Game.EnemySys.EnemyBehaviors
{
    /// <summary>
    /// i have attempted to chatgpt here, didnt work, i deleted it all and now i will write...
    /// </summary>
    public class EnemyCube : MonoBehaviour
    {
        public float MovementSpeed = 5f;
        public float DashSpeed = 20f;
        public float DashRange = 3f;

        private Rigidbody _rb;
        private Transform _player;
        private bool isDashing = false;
        private Vector3 _dashTarget;
        private NavMeshPath _navPath;
        Action CurrentAcion;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _rb = GetComponent<Rigidbody>();
            CurrentAcion = Move;
            _navPath = new NavMeshPath();
        }

        private void Update()
        {
            CurrentAcion();
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
                    if (Vector3.Distance(transform.position, _navPath.corners[i]) > 2f)
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

                Debug.Log("Target Position: " + targetPosition);
                Debug.Log("Direction: " + direction);

                _rb.velocity = direction * MovementSpeed;
            }
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
