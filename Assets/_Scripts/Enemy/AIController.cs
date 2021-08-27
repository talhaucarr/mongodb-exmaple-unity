using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Character;
using _Scripts.Character.Combat;
using _Scripts.Character.Vitality;
using UnityEngine;
using UnityEngine.AI;
using _Scripts.Managers;
using RPGCharacterAnimsFREE.Actions;


namespace _Scripts.Enemy
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance;

        private AttackModule _attackModule;
        private Health _health;
        private MovementModule _movementModule;

        private Vector3 _guardLocation;

        private void Start()
        {
            _guardLocation = transform.position;
            _attackModule = GetComponent<AttackModule>();
            _health = GetComponent<Health>();
            _movementModule = GetComponent<MovementModule>();
        }

        private void Update()
        {
            if(_health.IsDead()) return;
            
            if (InAttackRangeOfPlayer() && _attackModule.CanAttack(PlayerManager.Instance.Player.gameObject))
            {
                _attackModule.Attack(PlayerManager.Instance.Player.gameObject);
                Debug.Log("attack");
            }
            else
            {
                _movementModule.StartMoveAction(_guardLocation);
            }
            
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

