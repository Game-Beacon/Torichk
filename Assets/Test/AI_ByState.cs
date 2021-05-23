using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ByState : MonoBehaviour
{
    public float speed;
    public IdleState idleState;
    public MoveState moveState;
    public AttackState attackState;
    public Istate currentState;


    private void Start()
    {
        idleState = new IdleState(this);
        moveState =new MoveState(this);
        attackState = new AttackState(this);
        currentState = idleState;
    }
 

    void Update()
    {
        currentState.OnStateExecution();
        if (Vector2.Distance(transform.position,PlayerCtrl.PlayerPosition)<10)
        { ChangeState(attackState); }
    }

    public  void ChangeState(Istate nextstate)
    {
        currentState.OnstateExit();
        nextstate.OnStateEnter();
        currentState = nextstate;
    }

}




public class IdleState :Istate
{
    AI_ByState aiBystate;
    public IdleState(AI_ByState _aI_ByState)
    {

        aiBystate = _aI_ByState;
    }
    float timeEnd ;
    void Istate.OnStateEnter()
    {     
        timeEnd = Time.time + 3f;
        aiBystate.speed = 0;
        Debug.Log("IdleEnter");
    }

    void Istate.OnStateExecution()
    {
        Debug.Log("IdleEx");
        if (Time.time > timeEnd) {
            aiBystate.ChangeState(aiBystate.moveState);
        }
    }

    void Istate.OnstateExit()
    {
        Debug.Log("IdleExit");
    }
}

public class MoveState : Istate
{
    float timeEnd;
    AI_ByState aiBystate;
    public MoveState(AI_ByState aI_ByState)
    {
        aiBystate = aI_ByState;
    }
    void Istate.OnStateEnter()
    {
        timeEnd = Time.time + 3f;
        aiBystate.speed = 10;
        Debug.Log("MoveEnter");
    }

    void Istate.OnStateExecution()
    {
        if (Time.time > timeEnd)
        {
            aiBystate.ChangeState(aiBystate.idleState);
        }
        Debug.Log("MoveEx");
    }

    void Istate.OnstateExit()
    {
        Debug.Log("MoveExut");
    }
}

public class AttackState : Istate
{
    AI_ByState aistate;
    public AttackState(AI_ByState aI_ByState)
    {
        aistate = aI_ByState;
    }

    void Istate.OnStateEnter()
    {
        Debug.Log("AttackEnter");
    }

    void Istate.OnStateExecution()
    {
        Debug.Log("AttackEx");
    }

    void Istate.OnstateExit()
    {
        Debug.Log("AttackExit");
    }
}



public interface Istate
{
    void OnStateEnter();
    void OnStateExecution();
    void OnstateExit();
}
