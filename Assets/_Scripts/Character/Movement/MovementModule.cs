using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using _Scripts.Combat;
using _Scripts.Core;

namespace _Scripts.Character
{
    public class MovementModule : MonoBehaviour, IMovementModule
    {
        private ActionScheduler _actionScheduler;
        private IAttackModule _attackModule;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;


        private void Start()
        {
            _actionScheduler = GetComponent<ActionScheduler>();
            _attackModule = GetComponent<AttackModule>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
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
            _attackModule.AttackCancel();
            Move(destination);
        }

        public void Move(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            _navMeshAgent.isStopped = true;
        }
    }
}

