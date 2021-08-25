using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Scripts.Character;

namespace _Scripts.Combat
{
    public class AttackModule : MonoBehaviour, IAttackModule
    {
        [SerializeField] private float attackRange;

        private IMovementModule _movementModule;

        private Transform _enemy;

        private void Start()
        {
            _movementModule = GetComponent<MovementModule>();
        }

        private void Update()
        {
            if (_enemy == null)
                return;

            Debug.Log(IsInRange(_enemy));
            if (!IsInRange(_enemy))
                _movementModule.StartMoveAction(_enemy.position);
            
            else
                _movementModule.Stop();
        }
        public void Attack(GameObject enemy)
        {
            Debug.Log(enemy.name);
            _enemy = enemy.transform;            
        }

        public void AttackCancel()
        {
            _enemy = null;
        }
        private bool IsInRange(Transform enemy)
        {
            Debug.Log(Vector3.Distance(enemy.position, transform.position));
            return Vector3.Distance(enemy.position, transform.position) < attackRange;
        }
    }
}

