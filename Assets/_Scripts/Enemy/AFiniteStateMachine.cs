using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FSMStateType
{
    IDLE,
    RUN,
    PATROL,
    SUSPICION,
    ATTACK,
}
public abstract class AFiniteStateMachine : ScriptableObject
{
    protected FSMController _fsm;


    public FSMStateType StateType { get; protected set; }

    public virtual void OnEnable() { }

    public virtual bool EnterState() { return true; }

    public abstract void UpdateState();

    public virtual bool ExitState() { return true; }

    public virtual void SetFSM(FSMController fsm)
    {
        if (fsm != null)
        {
            _fsm = fsm;
        }
    }
}
