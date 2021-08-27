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

        private AttackModule _attackModule;
        private Health _health;
        private MovementModule _movementModule;
        private ActionScheduler _actionScheduler;

        private Vector3 _guardLocation;

        private float _timeSinceLastSawPlayer = Mathf.Infinity;
        
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
                GuardBehaviour();
            }
            _timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            _movementModule.StartMoveAction(_guardLocation);
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

