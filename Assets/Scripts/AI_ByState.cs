using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public Animator _animator;
    public SpriteRenderer _spriteRenderer;
    private AI_ByState aI_ByState;
    private void Start()
    {
        idleState = new IdleState(this);
        moveState =new MoveState(this);
        attackState = new AttackState(this);
        currentState = idleState;
        O = transform.position;
        AttackDistance = 8;
        Distance = 5;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        currentState.OnStateExecution();
        if (Vector2.Distance(transform.position,PlayerCtrl.PlayerPosition)<MaskCtrl.currectScal)
        { ChangeState(attackState); }
        AiPosition = transform.position;
        if (Vector2.Distance(transform.position, PlayerCtrl.PlayerPosition) < 1&& PlayerCtrl.IsScare ==false)
        {
            PlayerCtrl.IsScare = true;
            PlayerCtrl.CanMove = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag =="Player")
        {
            _animator.SetBool("IsAttack",true);
            other.transform.GetComponent<PlayerCtrl>().KillPlayer();
        }
    }
    public  void ChangeState(Istate nextstate)
    {
        currentState.OnstateExit();
        nextstate.OnStateEnter();
        currentState = nextstate;
    }
    public void MoveToPosition(Vector3 position,float v)
    {
        transform.position = Vector3.Lerp(transform.position,position,v);
    }
    
    public  AI_ByState GetAi()
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
    
    public  void SetState1()
    {
        _animator.SetBool("IsState2",false);
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
        aiBystate._animator.SetBool("IsMove",false);
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
    }
}

public class MoveState : Istate
{
    float timeEnd;
    AI_ByState aiBystate;
    Vector3 Target;
    bool dirold = true;
    public MoveState(AI_ByState aI_ByState)
    {
        aiBystate = aI_ByState;
    }
    void Istate.OnStateEnter()
    {
        timeEnd = Time.time + 3f;
        aiBystate.speed = 10;
        Target = aiBystate.O + new Vector3(Random.Range(-12,12),Random.Range(-12,12),0);
        if (aiBystate.AiPosition.x - Target.x > 0 != dirold)
        {
            aiBystate._spriteRenderer.flipX = !aiBystate._spriteRenderer.flipX;
            dirold = aiBystate.AiPosition.x - Target.x > 0;
        }
        aiBystate._animator.SetBool("IsMove",true);
    }

    void Istate.OnStateExecution()
    {
        aiBystate.MoveToPosition(Target,0.001f);
        if (Time.time > timeEnd)
        {
            aiBystate.ChangeState(aiBystate.idleState);
        }
    }

    void Istate.OnstateExit()
    {
    }
}

public class AttackState : Istate
{
    AI_ByState aistate;
    bool dirold = true;
    public AttackState(AI_ByState aI_ByState)
    {
        aistate = aI_ByState;
    }

    void Istate.OnStateEnter()
    {
    }

    void Istate.OnStateExecution()
    {

        aistate.MoveToPosition(PlayerCtrl.PlayerPosition, 0.001f);
        if (aistate.AiPosition.x - PlayerCtrl.PlayerPosition.x > 0 != dirold)
        {
            aistate._spriteRenderer.flipX = !aistate._spriteRenderer.flipX;
            dirold = aistate.AiPosition.x - PlayerCtrl.PlayerPosition.x > 0;
        }
        aistate._animator.SetBool("IsMove",true);
        
        
        if (Vector3.Distance(aistate.AiPosition,PlayerCtrl.PlayerPosition)>MaskCtrl.currectScal)
        {
            aistate.ChangeState(aistate.idleState);
        }
        if (Vector3.Distance(aistate.AiPosition,PlayerCtrl.PlayerPosition)<aistate.Distance)
        {
        }
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
