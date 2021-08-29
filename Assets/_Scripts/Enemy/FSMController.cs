using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using _Scripts.Character;
using _Scripts.Character.Combat;
using _Scripts.Character.Vitality;
using _Scripts.Core;
using _Scripts.Managers;
using _Scripts.Enemy;

public class FSMController : MonoBehaviour
{
    [SerializeField] private float chaseDistance;
    [SerializeField] private float suspicionTime;
    [SerializeField] private float waypointDwellTime;
    [SerializeField] private PatrolPath patrolPath;
    [SerializeField] private float wayPointTolerance;

    [Range(0, 1)]
    [SerializeField] private float patrolSpeedFraction = 0.2f;

    private AttackModule _attackModule;
    private Health _health;
    private MovementModule _movementModule;
    private ActionScheduler _actionScheduler;

    private Vector3 _guardLocation;

    private float _timeSinceLastSawPlayer = Mathf.Infinity;
    private float _timeSinceArrivedWaypoint = Mathf.Infinity;
    private int _currentWaypointIndex = 0;

    float timer  = 0.0f; // begins at this value
 float timerMax = 3.0f; // event occurs at this value
  

    [Header("Options")]
    [SerializeField] private AFiniteStateMachine startingState;
    [SerializeField] List<AFiniteStateMachine> validStates;

    [Header("Monitors")]
    [SerializeField] [ShowOnly] private AFiniteStateMachine _currentState;

    private Dictionary<FSMStateType, AFiniteStateMachine> _fsmStates;

    #region UNITY EVENT FUNCTIONS

    void Awake()
    {
        _currentState = null;
        _fsmStates = new Dictionary<FSMStateType, AFiniteStateMachine>();

        foreach (AFiniteStateMachine state in validStates)
        {
            state.SetFSM(this);//this, ilgili nesnenin referansini belirtir.
            _fsmStates.Add(state.StateType, state);
        }
    }

    void Start()
    {
        if (startingState != null)
        {
            EnterState(startingState);
        }

        _guardLocation = transform.position;
        _attackModule = GetComponent<AttackModule>();
        _health = GetComponent<Health>();
        _movementModule = GetComponent<MovementModule>();
        _actionScheduler = GetComponent<ActionScheduler>();

    }

    private void Update()
    {
        if (!_currentState) { return; }

        _currentState.UpdateState();

       


        if (_health.IsDead()) return;

        if (InAttackRangeOfPlayer() && _attackModule.CanAttack(PlayerManager.Instance.Player.gameObject))
        {
            _timeSinceLastSawPlayer = 0;
            EnterState(FSMStateType.ATTACK);
        }

        else if (_timeSinceLastSawPlayer < suspicionTime)
        {
            //Suspicion
            EnterState(FSMStateType.SUSPICION);
        }

        else
        {
            EnterState(FSMStateType.PATROL);
        }

        UpdateTimers();

    }

    private bool InAttackRangeOfPlayer()
    {
        return Vector3.Distance(PlayerManager.Instance.Player.position, transform.position) < chaseDistance;
    }
    private void UpdateTimers()
    {
        _timeSinceLastSawPlayer += Time.deltaTime;
        _timeSinceArrivedWaypoint += Time.deltaTime;
    }

    #endregion

    #region STATE MANAGEMENT

    public void EnterState(AFiniteStateMachine nextState)
    {
        if (nextState == null)
        {
            return;
        }
        if (_currentState != null)
        {
            _currentState.ExitState();
        }

        _currentState = nextState;
        _currentState.EnterState();
    }

    public void EnterState(FSMStateType stateType)
    {
        if (_fsmStates.ContainsKey(stateType))
        {
            AFiniteStateMachine nextState = _fsmStates[stateType];
            _currentState.ExitState();
            EnterState(nextState);
        }
    }

    #endregion
}