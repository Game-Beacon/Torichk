using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITest : MonoBehaviour
{
    Vector3 origin;
    float DefensiveDistance = 40;
    public const int AIThinkTime = 1;
    public const int AIDetectDistance = 10;
    public const int AIAttackDistance = 2;
    public float MOVE_SPEED = 0;


    private float LastThinkTime;
    Rigidbody2D rigibody2D;
    Vector3 moveDir;
    bool StateSwitch = false;


    bool IsMoveToOrigin = true;

    void Start()
    {       
    }

    void Update()
    {
        if (Vector3.Distance(origin, transform.position) < DefensiveDistance && IsMoveToOrigin)
        {
            Debug.Log("Mask" + PlayerCtrl.GetMaskDistance());
            if (Vector3.Distance(transform.position, PlayerCtrl.GetPlayerPosition()) < AIDetectDistance + PlayerCtrl.GetMaskDistance())
            {

                SetDir(PlayerCtrl.GetPlayerPosition());
                //animation_Move
                Debug.Log("Move");

                if (Vector3.Distance(transform.position, PlayerCtrl.GetPlayerPosition()) < AIAttackDistance + PlayerCtrl.GetMaskDistance())
                {
                    //animation_attack
                    PlayerCtrl.Isdeath = true;
                    Debug.Log("Attack");
                }

            }
            else
            {
                if (Time.time - LastThinkTime > AIThinkTime)
                {
                    Debug.Log("switch");
                    LastThinkTime = Time.time;

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
                            SetDir(new Vector3(Random.Range(-2, 2), Random.Range(-2, 2)));

                            break;
                        default:
                            break;
                    }
                }
            }
        }
        else {
            Debug.Log("OOOO");
            SetDir(origin);
            IsMoveToOrigin = false;
            if (Vector3.Distance(origin, transform.position) < 0.1|| Vector3.Distance(transform.position, PlayerCtrl.GetPlayerPosition()) < AIDetectDistance + PlayerCtrl.GetMaskDistance()) IsMoveToOrigin = true;

        }
    }



    void SetDir(Vector3 target) {
        Debug.Log(moveDir);
        moveDir = new Vector3(target.x-transform.position.x,target.y-transform.position.y).normalized;
 
    }

    private void Awake()
    {
        rigibody2D = GetComponent<Rigidbody2D>();
        origin = transform.position;

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
