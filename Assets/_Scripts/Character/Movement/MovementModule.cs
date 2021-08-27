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

        public void StartMoveAction(Vector3 destination)
        {
            _actionScheduler.StartAction(this);
            Move(destination);
        }

        public void Move(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }
    }
}

