using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ByState : MonoBehaviour
{
    public float speed;
    public float DefendDistance;//防守範圍(從出生點)
    public float AttackDistance;//偵查距離
    public Vector3 O;//出生點
    public IdleState idleState;
    public MoveState moveState;
    public AttackState attackState;
    public Istate currentState;
    public Vector3 AiPosition;

    private void Start()
    {
        idleState = new IdleState(this);
        moveState =new MoveState(this);
        attackState = new AttackState(this);
        currentState = idleState;
        O = transform.position;
        DefendDistance = 10;
        AttackDistance = 8;
    }
 

    void Update()
    {
        currentState.OnStateExecution();
        if (Vector2.Distance(transform.position,PlayerCtrl.PlayerPosition)<AttackDistance + MaskCtrl.currectScal * 3.5)
        { ChangeState(attackState); }
        AiPosition = transform.position;
        if (Vector3.Distance(AiPosition, O) > DefendDistance)
        {
            ChangeState(moveState);
        }
    }

    public  void ChangeState(Istate nextstate)
    {
        currentState.OnstateExit();
        nextstate.OnStateEnter();
        currentState = nextstate;
    }

    public void MoveToPosition(Vector3 position,float v) {
        transform.position = Vector3.Lerp(transform.position,position,v);
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
        Debug.Log(aiBystate.name+":IdleEnter");
    }

    void Istate.OnStateExecution()
    {
        Debug.Log(aiBystate.name + ":IdleEx");
        if (Time.time > timeEnd) {
            aiBystate.ChangeState(aiBystate.moveState);
        }
    }

   void Istate.OnstateExit()
    {
        Debug.Log(aiBystate.name + ":IdleExit");
    }
}

public class MoveState :Istate
{
    float timeEnd;
    AI_ByState aiBystate;
    Vector3 Target;
    public MoveState(AI_ByState aI_ByState)
    {
        aiBystate = aI_ByState;
    }
    void Istate.OnStateEnter()
    {
        timeEnd = Time.time + 3f;
        aiBystate.speed = 10;
        Debug.Log(aiBystate.name + ":MoveEnter");
        Target = aiBystate.O + new Vector3(Random.Range(-12,12),Random.Range(-12,12),0);
        while (Vector3.Distance(aiBystate.O, Target) > aiBystate.DefendDistance)
        {
         Debug.Log("aaaaaaa");
         Target = aiBystate.O + new Vector3(Random.Range(-12, 12), Random.Range(-12, 12),0);
        }
    }

    void Istate.OnStateExecution()
    {
        aiBystate.MoveToPosition(Target,0.001f);
        if (Time.time > timeEnd)
        {
            aiBystate.ChangeState(aiBystate.idleState);
        }
        Debug.Log(aiBystate.name + ":MoveEx");
    }

    void Istate.OnstateExit()
    {
        Debug.Log(aiBystate.name + ":MoveExut");
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
        Debug.Log(aistate.name + ":DetenceEnter");
    }

    void Istate.OnStateExecution()
    {
        if (Vector3.Distance(aistate.AiPosition, aistate.O) < aistate.DefendDistance)
        {
            aistate.MoveToPosition(PlayerCtrl.PlayerPosition, 0.001f);
        }
        else {
            aistate.MoveToPosition(aistate.O, 0.001f);
        }

        //aistate.MoveToPosition(PlayerCtrl.PlayerPosition,0.001f);
        if (Vector3.Distance(aistate.AiPosition,PlayerCtrl.PlayerPosition)>aistate.AttackDistance + MaskCtrl.currectScal * 3.5 || Vector3.Distance(aistate.AiPosition,aistate.O)>aistate.DefendDistance)
        {
            aistate.ChangeState(aistate.idleState);
        }
        Debug.Log(aistate.name + ":DetenceEx");
    }

    void Istate.OnstateExit()
    {
        Debug.Log(aistate.name + ":DetenceExit");
    }
}



public interface Istate
{
    void OnStateEnter();
    void OnStateExecution();
    void OnstateExit();
}
