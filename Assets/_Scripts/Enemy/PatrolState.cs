using _Scripts.Character;
using _Scripts.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PatrolState", menuName = "FSM/States/Idle")]
public class PatrolState : AFiniteStateMachine
{
    
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
        Debug.Log("Update Patrol State");

    }

    public override bool ExitState()
    {
        base.ExitState();
        return true;
    }

   
}
