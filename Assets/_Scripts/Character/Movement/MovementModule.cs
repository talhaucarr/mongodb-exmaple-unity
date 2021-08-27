using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using _Scripts.Character.Combat;
using _Scripts.Character.Vitality;
using _Scripts.Core;

namespace _Scripts.Character
{
    public class MovementModule : MonoBehaviour, IMovementModule, IAction
    {
        [SerializeField] private float maxSpeed = 6f;
        
        private ActionScheduler _actionScheduler;
        private AttackModule _attackModule;
        
        
        private NavMeshAgent _navMeshAgent;
        private Health _health;
        private Animator _animator;
        
        private void Start()
        {
            _actionScheduler = GetComponent<ActionScheduler>();
            _attackModule = GetComponent<AttackModule>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health>();
        }

        private void Update()
        {
            _navMeshAgent.enabled = !_health.IsDead();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = _navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            _animator.SetFloat("forwardSpeed", speed);
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            _actionScheduler.StartAction(this);
            Move(destination, speedFraction);
        }

        public void Move(Vector3 destination, float speedFraction)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            _navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }
    }
}

