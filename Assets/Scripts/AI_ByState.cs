using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ByState : MonoBehaviour
{
    public float speed;
    public float AttackDistance;//追擊距離
    public float Distance;//攻擊距離
    public Vector3 O;//出生點
    public IdleState idleState;
    public MoveState moveState;
    public AttackState attackState;
    public Istate currentState;
    public Vector3 AiPosition;
    public Sprite img1;

    private static AI_ByState aI_ByState;

    private void Start()
    {
        idleState = new IdleState(this);
        moveState =new MoveState(this);
        attackState = new AttackState(this);
        currentState = idleState;
        O = transform.position;
        AttackDistance = 8;
        Distance = 5;
    }
 

    void Update()
    {
        currentState.OnStateExecution();
        if (Vector2.Distance(transform.position,PlayerCtrl.PlayerPosition)<MaskCtrl.currectScal * 3.5)
        { ChangeState(attackState); }
        AiPosition = transform.position;
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



    public static AI_ByState GetAi()
    {
        if (aI_ByState == null)
        {
            return aI_ByState = new AI_ByState();
        }
        else
        {
            return aI_ByState;
        }
    }

    internal void ChangeImage(PlayerCtrl playerT, PlayerEventArgs e)
    {
        if (e.ObjectCount == 4)
        {
            Debug.Log("ChangeToImage2");//尚未處理
        }
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

        aistate.MoveToPosition(PlayerCtrl.PlayerPosition, 0.001f);
        if (Vector3.Distance(aistate.AiPosition,PlayerCtrl.PlayerPosition)>MaskCtrl.currectScal * 3.5)
        {
            aistate.ChangeState(aistate.idleState);
        }
        if (Vector3.Distance(aistate.AiPosition,PlayerCtrl.PlayerPosition)<aistate.Distance)
        {
            Debug.Log("KillPlayer");
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
