using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICtrl : MonoBehaviour
{
    MonsterState monsterState = MonsterState.alert;
    public float MOVE_SPEED = 50f;
    private Rigidbody2D rigibody2D;
    private Vector3 moveDir;
    enum MonsterState {
        alert,//警戒
        patrol,//巡邏
        closeTo,//接近
        attack
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        rigibody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool goPoint = false;
        Vector2 point = new Vector2();

        int ChangeState;


        switch (monsterState)
        {
            case MonsterState.alert:
                ChangeState = Time.frameCount+120;
                if (Time.frameCount>ChangeState) {
                    monsterState = ChangeMonsterState();
                    Debug.Log("TTT");
                }
                Debug.Log("alert");
                if (Mathf.Abs(PlayerCtrl.GetPlayerPosition().x - transform.position.x) <= 1 && Mathf.Abs(PlayerCtrl.GetPlayerPosition().y - transform.position.y) <= 1)
                {
                    Debug.Log("" + Mathf.Abs(PlayerCtrl.GetPlayerPosition().x - transform.position.x));
                    Debug.Log("" + Mathf.Abs(PlayerCtrl.GetPlayerPosition().y - transform.position.y));

                    monsterState = MonsterState.attack;
                }
                break;
            case MonsterState.patrol:
                if (goPoint)
                {
                    moveDir = point.normalized;
                }
                else
                {
                    point = new Vector3(Random.Range(-8, 8), Random.Range(-8, 8)).normalized;
                    goPoint = true;
                }

                Debug.Log("patrol");
                if (Mathf.Abs(PlayerCtrl.GetPlayerPosition().x - transform.position.x) <= 1 || Mathf.Abs(PlayerCtrl.GetPlayerPosition().y - transform.position.y) <= 1)
                {
                    monsterState = MonsterState.attack;
                }

                break;

            case MonsterState.closeTo:
                moveDir = new Vector3(PlayerCtrl.GetPlayerPosition().x - transform.position.x, PlayerCtrl.GetPlayerPosition().y - transform.position.y).normalized;
                Debug.Log("closeto");
                if (Mathf.Abs(PlayerCtrl.GetPlayerPosition().x - transform.position.x) <= 1 || Mathf.Abs(PlayerCtrl.GetPlayerPosition().y - transform.position.y) <= 1)
                {
                    monsterState = MonsterState.attack;
                }
                break;

            case MonsterState.attack:
                PlayerCtrl.Hp = false;
                Debug.Log("attack");
                if (Mathf.Abs(PlayerCtrl.GetPlayerPosition().x - transform.position.x) <= 1 || Mathf.Abs(PlayerCtrl.GetPlayerPosition().y - transform.position.y) <= 1)
                {
                    monsterState = MonsterState.attack;
                }
                break;

            default:

                break;
        }


    }


    MonsterState ChangeMonsterState() {
       return (MonsterState)Random.Range(0,1);
    }

    private void FixedUpdate()
    {
        rigibody2D.velocity = moveDir * MOVE_SPEED;
    }

}
