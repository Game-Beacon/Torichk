using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerCtrl : MonoBehaviour
{
    public static Vector3 PlayerPosition;
    public  float MoveSpeed;
    private Rigidbody2D rigibody2D;
    private Vector3 moveDir;
    public static bool Isdeath = false;
    public static bool PlayerIsMove;
    public static bool PlayerIsRun;
    public  PlayerCtrl playerT;
    public MaskCtrl maskt;
    static SpriteRenderer spriteRenderer;
    public Animator animator;
    public static bool IsScare = false;
    public  static bool CanMove;
    public GameObject Foxsigel;
    private void Start()
    {
        rigibody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        maskt = GetComponent<MaskCtrl>();
        playerT = GetComponent<PlayerCtrl>();
    }

    private void Awake()
    {
        PlayerIsRun = false;
        MoveSpeed = 2f;
        objectCount = 0;
        CanMove = true;
        playerT.objectCountChange += maskt.ChangeMaskD;
        playerT.objectCountChange += maskt.ChangeMaskDistance;//這三行訂閱ObjectCount狀態有沒有發生改變
    }
    void Update()
    {
        animator.SetBool("IsScare",IsScare);
        if (CanMove)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                animator.SetFloat("Speed",1.0f);
                PlayerIsRun = true;
                MoveSpeed = 10;
            }
            else
            {
                animator.SetFloat("Speed",0.3f);

                PlayerIsRun = false;
                MoveSpeed = 4;
            }
            
            PlayerPosition = transform.position;
            float moveX = 0f;
            float moveY = 0f;
            if (Input.GetKey(KeyCode.UpArrow)) moveY = 1f;
            if (Input.GetKey(KeyCode.DownArrow)) moveY = -1f;
            if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
            if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;
            moveDir = new Vector3(moveX, moveY).normalized;

            if (Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.DownArrow)||Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.RightArrow))
            {

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Foxsigel.transform.position = PlayerPosition+ new Vector3(-0.175f,0.048f,0);
                    spriteRenderer.flipX = false;
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    Foxsigel.transform.position = PlayerPosition+ new Vector3(0.175f,0.048f,0);
                    spriteRenderer.flipX = true;
                }
                PlayerIsMove = true;
                animator.SetBool("IsRun",true);
            }
            else
            {
                PlayerIsMove = false;
                animator.SetBool("IsRun",false);
            }
            
        }
        else
        {
            MoveSpeed = 0;
        }



        if (Input.GetKeyDown(KeyCode.A))//按A可以放大光圈
        {
            LevelUp();
        }
    }

    private void FixedUpdate()
    {
        rigibody2D.velocity = moveDir*MoveSpeed;
    }
    private int objectCount;
    public int ObjectCount
    {
        get { return objectCount; }
        set
        {
            if (value >= 0 && value < 3)//目前陣列最高是3個，濾掉不符合的值
            {
                objectCount = value;
                PlayerEventArgs playerEventArgs = new PlayerEventArgs();
                playerEventArgs.ObjectCount = objectCount;
                objectCountChange.Invoke(this, playerEventArgs);//狀態有發生變化，傳遞變化後的狀態給訂閱者
                if (value ==2)
                {
                    ChangeMonsterStateTo1();
                }
            }
        }
    }
    private ObjectCountChangeHandler objectCountChange;
    public event ObjectCountChangeHandler Change
    {
        add
        {
            this.objectCountChange += value;
        }
        remove
        {
            this.objectCountChange -= value;
        }
    }
    public   void LevelUp() {
            playerT =  GetComponent<PlayerCtrl>();
            animator = GetComponent<Animator>();
            GameObject gam = transform.GetChild(0).gameObject;
            maskt =gam.GetComponent<MaskCtrl>();
            playerT.ObjectCount += 1;
    }

    public void KillPlayer()
    {
        animator.SetBool("IsDie",true);
        MapV2.MonsterList.Clear();
        ChangeMap.LampBeUse = false;
        //ReloadSC();
    }

    public void ChangeMonsterStateTo1()
    {
        foreach (var mon in MapV2.MonsterList)
        {
            mon.GetComponent<AI_ByState>().SetState1();
        }
    }
    void ReloadSC()
    {
        IsScare = false;
        // ChangeMap.LampBeUse = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ///關掉怪物跟玩家的移動   
    }
}
public delegate void ObjectCountChangeHandler(PlayerCtrl playerT, PlayerEventArgs e);
public class PlayerEventArgs : EventArgs
{
    public int ObjectCount { get; set; }
}

