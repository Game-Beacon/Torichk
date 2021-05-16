using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ByState : MonoBehaviour
{
    AttackState attackstate = new AttackState();
    IdleState IdleState = new IdleState();
    RunState runState = new RunState();
    public Istate currentState ;

    // Start is called before the first frame update
    void Start()
    {
        currentState = IdleState;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateExecution();
        if (Vector2.Distance(transform.position,PlayerCtrl.GetPlayerPosition())<1)
        {
            ChangeState(attackstate);
        }
    }

    void ChangeState(Istate nextstate) {
        currentState.OnstateExit();
        nextstate.OnStateEnter();
        currentState = nextstate;
    }
}



class IdleState :MonoBehaviour, Istate
{
    void Istate.OnStateEnter()
    {
    }

    void Istate.OnStateExecution()
    {
    }

    void Istate.OnstateExit()
    {
    }
}


class AttackState : Istate
{
    void Istate.OnStateEnter()
    { 
    }

    void Istate.OnStateExecution()
    {
    }

    void Istate.OnstateExit()
    {
    }
}

class RunState : MonoBehaviour, Istate
{
    void Istate.OnStateEnter()
    {
        throw new System.NotImplementedException();
    }

    void Istate.OnStateExecution()
    {
        throw new System.NotImplementedException();
    }

    void Istate.OnstateExit()
    {
        throw new System.NotImplementedException();
    }
}

public interface Istate
{
    void OnStateEnter();
    void OnStateExecution();
    void OnstateExit();
}
