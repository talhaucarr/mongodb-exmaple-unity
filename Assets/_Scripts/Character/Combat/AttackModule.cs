using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Scripts.Character;
using _Scripts.Character.Vitality;
using _Scripts.Core;
using UnityEngine.Serialization;

namespace _Scripts.Character.Combat
{
    public class AttackModule : MonoBehaviour, IAttackModule, IAction
    {
        [SerializeField] private float timeBetweenAttacks;
        [SerializeField] private Transform rightHandTransform = null;
        [SerializeField] private Transform leftHandTransform = null;
        [SerializeField] private Weapon defaultWeapon = null;

        private Weapon _currentWeapon = null;
        
        private IMovementModule _movementModule;
        private ActionScheduler _actionScheduler;
        private Animator _animator;
        private Health _enemy;

        private float _timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            _movementModule = GetComponent<MovementModule>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
            
            EquipWeapon(defaultWeapon);
        }

        public void EquipWeapon(Weapon weapon)
        {
            _currentWeapon = weapon;
            //if(weapon == null) return;
            weapon.Spawn(rightHandTransform,leftHandTransform, _animator);
        }
        

        private void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
            
            if (_enemy == null)
                return;
            
            if(_enemy.IsDead()) return;
            
            if (!IsInRange())
                _movementModule.Move(_enemy.transform.position, 1f);

            else
            {
                _movementModule.Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(_enemy.transform);
            if (!(_timeSinceLastAttack > timeBetweenAttacks)) return;
            
            TriggerAttack();
            _timeSinceLastAttack = 0;
            
        }

        private void TriggerAttack()
        {
            _animator.ResetTrigger("attackCancel");
            _animator.SetTrigger("attack");
        }

        public bool CanAttack(GameObject enemy)
        {
            Debug.Log(enemy);
            if (enemy == null)
                return false;
            Health test = enemy.GetComponent<Health>();
            return test != null && !test.IsDead();
        }
        
        public void Attack(GameObject enemy)
        {
            Debug.Log(enemy.name);
            _actionScheduler.StartAction(this);
            _enemy = enemy.GetComponent<Health>();            
        }
        
        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, _enemy.transform.position) < _currentWeapon.AttackRange();
        }

        public void Cancel()
        {
            StopAttack();
            _enemy = null;
            _movementModule.Cancel();
        }

        private void StopAttack()
        {
            _animator.ResetTrigger("attack");
            _animator.SetTrigger("attackCancel");
        }

        //Animation Event
        void Hit()//TODO: damage before the projectile reaches
        {
            if(_enemy == null) return;
            
            if (_currentWeapon.HasProjectile())
                _currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, _enemy, gameObject);

            else
                _enemy.TakeDamage(gameObject ,_currentWeapon.AttackDamage());
        }

        void Shoot()
        {
            Hit();
        }
    }
}

