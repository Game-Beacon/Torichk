using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ByState : MonoBehaviour
{
    public static float speed;
    AttackState attackState = new AttackState();
    public static IdleState idleState = new IdleState();
    public static MoveState moveState = new MoveState();
    public static Istate currentState ;

    // Start is called before the first frame update
    void Start()
    {
        currentState = idleState;
        speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateExecution();
        if (Vector2.Distance(transform.position,PlayerCtrl.PlayerPosition)<1)
        {
            ChangeState(attackState);
        }
    }

    void ChangeState(Istate nextstate) {
        currentState.OnstateExit();
        nextstate.OnStateEnter();
        currentState = nextstate;
    }
}



public class IdleState :MonoBehaviour, Istate
{
    //待機3秒idle或move
    float timeEnd ;
    void Istate.OnStateEnter()
    {
        timeEnd = Time.time + 3;
        AI_ByState.speed = 0;
    }

    void Istate.OnStateExecution()
    {

        if (Time.time<timeEnd)
        {
            if (Random.Range(0,1)>=1)
            {
                AI_ByState.currentState = AI_ByState.idleState;
            }
            else
            {
                AI_ByState.currentState = AI_ByState.moveState;
            }
        }

    }

    void Istate.OnstateExit()
    {
    }
}


public class AttackState : Istate
{
    void Istate.OnStateEnter()
    { 
    }

    void Istate.OnStateExecution()
    {
        PlayerCtrl.Isdeath = true;
    }

    void Istate.OnstateExit()
    {
    }
}

public class MoveState : MonoBehaviour, Istate
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

public interface Istate
{
    void OnStateEnter();
    void OnStateExecution();
    void OnstateExit();
}
