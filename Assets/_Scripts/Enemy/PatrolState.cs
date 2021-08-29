using _Scripts.Character;
using _Scripts.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PatrolState", menuName = "FSM/States/Idle")]
public class PatrolState : AFiniteStateMachine
{
    [SerializeField] private Transform player;
    [SerializeField] private float chaseDistance;
    [SerializeField] private float suspicionTime;
    [SerializeField] private float waypointDwellTime;
    [SerializeField] private PatrolPath patrolPath;
    [SerializeField] private float wayPointTolerance;

    [Range(0, 1)]
    [SerializeField] private float patrolSpeedFraction = 0.2f;

    private Vector3 _guardLocation;

    private float _timeSinceLastSawPlayer = Mathf.Infinity;
    private float _timeSinceArrivedWaypoint = Mathf.Infinity;
    private int _currentWaypointIndex = 0;

    private MovementModule _movementModule;

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.IDLE;
    }

    public override bool EnterState()
    {
        base.EnterState();

        return true;
    }
    public override void UpdateState()
    {

        PatrolBehaviour();
        Debug.Log("Update Patrol State");

    }

    public override bool ExitState()
    {
        base.ExitState();
        return true;
    }

    private void PatrolBehaviour()
    {
        var nextPos = _guardLocation;

        if (patrolPath != null)
        {
            if (AtWayPoint())
            {
                _timeSinceArrivedWaypoint = 0;
                CycleWayPoint();
            }

            nextPos = GetCurrentWayPoint();
        }

        if (_timeSinceArrivedWaypoint > waypointDwellTime)
        {
            _movementModule.StartMoveAction(nextPos, patrolSpeedFraction);
        }


    }
    private Vector3 GetCurrentWayPoint()
    {
        return patrolPath.GetWayPoint(_currentWaypointIndex);
    }

    private void CycleWayPoint()
    {
        _currentWaypointIndex = patrolPath.GetNextIndex(_currentWaypointIndex);
    }

    private bool AtWayPoint()
    {
        var distanceToWaypoint = Vector3.Distance(player.position, GetCurrentWayPoint());
        return distanceToWaypoint < wayPointTolerance;
    }
}
