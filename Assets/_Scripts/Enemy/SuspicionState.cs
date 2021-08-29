using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Scripts.Enemy;

[CreateAssetMenu(fileName = "SuspicionState", menuName = "FSM/States/Suspicion")]
public class SuspicionState : AFiniteStateMachine
{

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.SUSPICION;
    }


    public override bool EnterState()
    {
        
        base.EnterState();

        return true;
    }
    public override void UpdateState()
    {


        Debug.Log("Update Suspicion State");

    }

    public override bool ExitState()
    {
        base.ExitState();
        return true;
    }
}
