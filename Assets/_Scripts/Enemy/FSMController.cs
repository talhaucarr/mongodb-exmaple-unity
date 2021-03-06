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
    }

    private void Update()
    {
        if (!_currentState) { return; }

        _currentState.UpdateState();
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