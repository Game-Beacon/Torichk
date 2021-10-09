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
    //public GameObject Foxsigel;
    public static PlayerCtrl Player;
    public float AniSpeed;
    public float DieSpeed;
    private bool AniLock;
    private void Start()
    {
        rigibody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        maskt = GetComponent<MaskCtrl>();
        playerT = GetComponent<PlayerCtrl>();
        IsScare = false;
        Player = playerT;
        DieSpeed = 0.1f;
        AniLock = true;
        PlayerPosition = transform.position;
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
        PlayerPosition = transform.position;

        if (CanMove)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                animator.SetFloat("Speed",0.3f);
                PlayerIsRun = true;
                MoveSpeed = 2.5f;
            }
            else
            {
                animator.SetFloat("Speed",0.1f);
                PlayerIsRun = false;
                MoveSpeed = 1.5f;
            }
            
            float moveX = 0f;
            float moveY = 0f;
            if (Input.GetKey(KeyCode.UpArrow)) moveY = 1f;
            if (Input.GetKey(KeyCode.DownArrow)) moveY = -1f;
            if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
            if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;
            moveDir = new Vector3(moveX, moveY).normalized;

            if (Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.DownArrow)||Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.RightArrow))
            {
                MaskCtrl.CanTo0 = true;
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    spriteRenderer.flipX = false;
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    spriteRenderer.flipX = true;
                }
                PlayerIsMove = true;
                animator.Play("Gumi_Run");
            }
            else
            {
                PlayerIsMove = false;
                animator.Play("Gumi_Idle");
            }

        }
        else
        {
            MoveSpeed = 0;
        }
        

        if (Input.GetKeyDown(KeyCode.A))//按A可以放大光圈
        {
            
            foreach (var lamp in MapV2.LamplList)
            {
                Debug.Log(lamp.name);
                if (Vector2.Distance((Vector2)lamp.transform.position,PlayerCtrl.PlayerPosition)<1)
                {
                    lamp.GetComponent<Lamp>().UseLamp();
                }
            }
        }
    }

    
    public static void SetCanDFalse()
    {
        Player.animator.SetBool("CanD",false);
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
        if (AniLock)
        {
            animator.Play("Gumi_Die2");
        }
        MapV2.ClearList();
        ChangeMap.LampBeUse = false;
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
        ChangeMap.gameData.Current = SceneManager.GetActiveScene().name;//暫存當前地圖
        SceneManager.LoadScene("UI_Six");//前往第中繼圖
        ///關掉怪物跟玩家的移動   
    }
}
public delegate void ObjectCountChangeHandler(PlayerCtrl playerT, PlayerEventArgs e);
public class PlayerEventArgs : EventArgs
{
    public int ObjectCount { get; set; }
}

