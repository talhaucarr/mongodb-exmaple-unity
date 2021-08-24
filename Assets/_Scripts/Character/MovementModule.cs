using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementModule : MonoBehaviour, IMovementModule
{
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Move(Vector3 destination)
    {
        _navMeshAgent.destination = destination;
    }
}
