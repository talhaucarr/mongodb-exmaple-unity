using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Scripts.Character;
using _Scripts.Character.Vitality;
using _Scripts.Core;
namespace _Scripts.Character.Combat
{
    public class AttackModule : MonoBehaviour, IAttackModule, IAction
    {
        [SerializeField] private float attackRange;
        [SerializeField] private float timeBetweenAttacks;

        private IMovementModule _movementModule;
        private ActionScheduler _actionScheduler;
        private Animator _animator;

        private Health _health;
        
        private Transform _enemy;

        private float _timeSinceLastAttack = 0;

        private void Start()
        {
            _movementModule = GetComponent<MovementModule>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            
            if (_enemy == null)
                return;
            
            if (!IsInRange())
                _movementModule.Move(_enemy.position);

            else
            {
                _movementModule.Cancel();
                AttackBehaviour();
            }
                
        }

        private void AttackBehaviour()
        {
            if (!(_timeSinceLastAttack > timeBetweenAttacks)) return;
            _animator.SetTrigger("attack");
            _timeSinceLastAttack = 0;
            
        }
        
        public void Attack(GameObject enemy)
        {
            Debug.Log(enemy.name);
            _actionScheduler.StartAction(this);
            _enemy = enemy.transform;            
        }

        
        
        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, _enemy.position) < attackRange;
        }

        public void Cancel()
        {
            _enemy = null;
        }
        
        //Animation Event
        void Hit()
        {
            _enemy.GetComponent<Health>().TakeDamage(10);//weapon damage
        }
        
    }
}

