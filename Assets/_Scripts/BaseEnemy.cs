using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


namespace _Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BaseEnemy : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        [SerializeField] private float _speed,
            _detectRadius,
            _attackRadius,
            _health,
            _timer,
            _timerReset;

        private float _maxHealth = 100;
        [SerializeField] private GameObject[] _patrolPoint, _instantiatePoints;
        private Vector3 _patrolDestination, thisPos;
        private GameObject _player;
        [SerializeField] private GameObject projectile;
        private bool playerInSightRadius, playerInAttackRadius, destinationSet;
        private State currentEnemyState;
        public LayerMask playerLayer;
        [SerializeField] private int scoreValue = 1;
        [SerializeField] private GameObject shootEffect;


        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _speed;
        }

        private void Start()
        {
            _player = GameObject.Find("Player");
        }

        private void Update()
        {
            thisPos = transform.position;

            playerInAttackRadius = Physics.CheckSphere(thisPos, _attackRadius, playerLayer);
            playerInSightRadius = Physics.CheckSphere(thisPos, _detectRadius, playerLayer);

            SetState();
        }


        private void SetState()
        {
            if (!playerInAttackRadius && !playerInSightRadius)
            {
                Patrol();
            }
            else if (playerInSightRadius && !playerInAttackRadius)
            {
                Follow();
            }
            else if (playerInAttackRadius && playerInSightRadius)
            {
                Attack();
            }
        }

        private void Patrol()
        {
            if (!destinationSet) GetNewPatrolPosition();

            if (destinationSet)
                _agent.SetDestination(_patrolDestination);

            if ((thisPos - _patrolDestination).magnitude < 2.5f)
                destinationSet = false;
        }

        private void Follow()
        {
            _agent.SetDestination(_player.transform.position);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void Attack()
        {
            _agent.SetDestination(thisPos);
            transform.LookAt(_player.transform.position + new Vector3(0, 0.5f, 0));
            _timer -= Time.deltaTime;
            if (!(_timer <= 0)) return;
            Instantiate(shootEffect, thisPos + transform.forward, Quaternion.identity);
            var rb = Instantiate(projectile, thisPos + transform.forward, Quaternion.identity)
                .GetComponent<Rigidbody>();
            rb.AddRelativeForce(transform.forward * 5000f * Time.deltaTime, ForceMode.Impulse);
            rb.AddRelativeForce(transform.up * 200f * Time.deltaTime, ForceMode.Impulse);
            _timer = _timerReset;
        }

        private void GetNewPatrolPosition()
        {
            _patrolDestination = _patrolPoint[Random.Range(0, _patrolPoint.Length)].transform.position;
            _agent.SetDestination(_patrolDestination);
            destinationSet = true;
        }


        public void ApplyDamage(float damage)
        {
            _health -= damage;
            print(_health);
            print("Ouch!!");
            if (_health <= 0)
            {
                UIManager.uiManagerInstance.UpdateScore(scoreValue);
                StartCoroutine(nameof(UpgradeMe));
            }
        }

        private IEnumerator UpgradeMe()
        {
            transform.position = new Vector3(1000, 1000, 1000);
            yield return new WaitForSeconds(5);
            thisPos = _instantiatePoints[Random.Range(0, _instantiatePoints.Length)].transform.position + transform.up;
            _health = _maxHealth;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(thisPos + transform.forward, .5f);
            Gizmos.color = Color.grey;
            Gizmos.DrawWireSphere(thisPos, _detectRadius);
        }
    }
}