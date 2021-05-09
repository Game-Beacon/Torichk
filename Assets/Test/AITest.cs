using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITest : MonoBehaviour
{

    public const int AIThinkTime = 1;
    public const int AIDetectDistance = 10;
    public const int AIAttackDistance = 2;
    public float MOVE_SPEED = 0;
    //public Vector3 origin;

    private float LastThinkTime;
    Rigidbody2D rigibody2D;
    Vector3 moveDir;
    bool StateSwitch = false;
    private MonsterState monsterState;

    void Start()
    {       
    }

    void Update()
    {       
        if (Vector3.Distance(transform.position,PlayerCtrl.GetPlayerPosition())<AIDetectDistance)
        {
            monsterState = MonsterState.Move;
            SetDir(PlayerCtrl.GetPlayerPosition());
            //animation_Move
            Debug.Log("Move");

            if (Vector3.Distance(transform.position, PlayerCtrl.GetPlayerPosition()) < AIAttackDistance)
            {
                monsterState = MonsterState.Attack;
                //animation_attack
                PlayerCtrl.Isdeath = true;
                Debug.Log("Attack");
            }
            
        }
        else
        {
            if (Time.time-LastThinkTime>AIThinkTime)
            {
                Debug.Log("switch");
                LastThinkTime = Time.time;
                //int Rand = Random.Range(0,1);

                switch (StateSwitch)
                {
                    case true:
                        Debug.Log("stand");
                        StateSwitch = !StateSwitch;
                        //animation_idle

                        break;
                    case false:
                        Debug.Log("move_");
                        StateSwitch = !StateSwitch;
                        //animation_Move
                        SetDir( new Vector3(Random.Range(-2, 2), Random.Range(-2, 2)));

                        break;
                    default:
                        break;
                }
            }

        }
        //moveDir = new Vector3(1,1,0).normalized;

    }



    void SetDir(Vector3 target) {
        Debug.Log(moveDir);
        moveDir = new Vector3(target.x-transform.position.x,target.y-transform.position.y).normalized;
 
    }

    private void Awake()
    {
        rigibody2D = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        rigibody2D.velocity = moveDir * MOVE_SPEED;
        Debug.Log("V:"+rigibody2D.velocity);
    }

     enum  MonsterState{
        Stand,
        Move,
        Attack,
    }
}
