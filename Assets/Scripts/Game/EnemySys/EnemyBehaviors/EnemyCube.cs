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
        public float DashCD = 5f;

        private Rigidbody _rb;
        private Transform _player;
        private Vector3 _dashTarget;
        private float _lastDash;
        private int _safetyticks;
        [SerializeField]
        private int SafetyLimit = 500;
        private NavMeshPath _navPath;

        /// <summary>
        /// me discovering Action be like:
        /// *proceeds with "i dont want to play with you" meme template on everything else*
        /// </summary>
        Action CurrentAcion;

        Vector3 _moveDir;

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
        private void FixedUpdate()
        {
            _rb.velocity = _moveDir;
            if(CurrentAcion == Dash)
            {
                if (_safetyticks < SafetyLimit)
                {
                    _safetyticks++;
                }
                else
                    CurrentAcion = Move;
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

                _moveDir = direction * MovementSpeed;
                _moveDir.y = 0;
                if(_moveDir != Vector3.zero)
                    transform.rotation = Quaternion.LookRotation(_moveDir, Vector3.up);
            }


            if (_lastDash < Time.time - DashCD)
            {
                if (Vector3.Distance(transform.position, _player.position) < DashRange)
                {
                    _lastDash = Time.time;
                    _dashTarget = _player.position;
                    _dashTarget = transform.position + (_dashTarget - transform.position).normalized * (DashRange + 20);
                    _safetyticks = 0;
                    CurrentAcion = Dash;
                }
            }
        }

        private void Dash()
        {
            if(!(Vector3.Distance(transform.position, _dashTarget) < 3))
            {
                _moveDir = (_dashTarget - transform.position).normalized * DashSpeed;
            }
            else
            {
                CurrentAcion = Move;
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

        private void OnCollisionEnter(Collision collision)
        {
            if(CurrentAcion == Dash)
            {
                switch(collision.transform.tag)
                {
                    case "Obstacle":
                        CurrentAcion = Move;
                        break;
                    case "Player":
                    case "Enemy":
                        collision.transform.SendMessage("TakeDamage", 30, SendMessageOptions.DontRequireReceiver);
                        break;

                }

            }
            else if(CurrentAcion == Move)
            {
                collision.transform.SendMessage("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
