using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementModule : MonoBehaviour, IMovementModule
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    

    private void Start()
    {
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

    public void Move(Vector3 destination)
    {
        _navMeshAgent.destination = destination;
    }
}
