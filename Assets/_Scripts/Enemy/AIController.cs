using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Character;
using _Scripts.Character.Combat;
using _Scripts.Character.Vitality;
using _Scripts.Core;
using UnityEngine;
using UnityEngine.AI;
using _Scripts.Managers;
using RPGCharacterAnimsFREE.Actions;


namespace _Scripts.Enemy
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance;
        [SerializeField] private float suspicionTime;
        [SerializeField] private float waypointDwellTime;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float wayPointTolerance;
        
        [Range(0,1)]
        [SerializeField] private float patrolSpeedFraction = 0.2f;

        private AttackModule _attackModule;
        private Health _health;
        private MovementModule _movementModule;
        private ActionScheduler _actionScheduler;

        private Vector3 _guardLocation;

        private float _timeSinceLastSawPlayer = Mathf.Infinity;
        private float _timeSinceArrivedWaypoint = Mathf.Infinity;
        private int _currentWaypointIndex = 0;
        
        private void Start()
        {
            _guardLocation = transform.position;
            _attackModule = GetComponent<AttackModule>();
            _health = GetComponent<Health>();
            _movementModule = GetComponent<MovementModule>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            if(_health.IsDead()) return;
            
            if (InAttackRangeOfPlayer() && _attackModule.CanAttack(PlayerManager.Instance.Player.gameObject))
            {
                _timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            
            else if (_timeSinceLastSawPlayer < suspicionTime)
            {
                //Suspicion
                SuspicionBehaviour();
            }
            
            else
            {
                PatrolBehaviour();
            }
            
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            _timeSinceLastSawPlayer += Time.deltaTime;
            _timeSinceArrivedWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            var nextPos = _guardLocation;

            if (patrolPath != null)
            {
                if (AtWayPoint())
                {
                    _timeSinceArrivedWaypoint = 0;
                    CycleWayPoint();
                }

                nextPos = GetCurrentWayPoint();
            }

            if (_timeSinceArrivedWaypoint > waypointDwellTime)
            {
                _movementModule.StartMoveAction(nextPos, patrolSpeedFraction);
            }
        }

        private Vector3 GetCurrentWayPoint()
        {
            return patrolPath.GetWayPoint(_currentWaypointIndex);
        }

        private void CycleWayPoint()
        {
            _currentWaypointIndex = patrolPath.GetNextIndex(_currentWaypointIndex);
        }

        private bool AtWayPoint()
        {
            var distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWayPoint());
            return distanceToWaypoint < wayPointTolerance;
        }

        private void SuspicionBehaviour()
        {
            _actionScheduler.CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            _attackModule.Attack(PlayerManager.Instance.Player.gameObject);
        }

        private bool InAttackRangeOfPlayer()
        {
            return Vector3.Distance(PlayerManager.Instance.Player.position, transform.position) < chaseDistance;
        }

        private void OnDrawGizmos()//OnDrawGizmosSelected
        {
            
            if (InAttackRangeOfPlayer())
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, chaseDistance);
            }

            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, chaseDistance);
            }
                
        }
    }
}

