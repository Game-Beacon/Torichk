using System;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCtrl : MonoBehaviour
{
    public static float MaskDistance;
    public static Vector3 PlayerPosition;
    public static float MoveSpeed;
    private Rigidbody2D rigibody2D;
    private Vector3 moveDir;
    public static bool Isdeath = false;
    public static bool PlayerIsRun;
    //..........................
    private static PlayerCtrl playerT;
    MaskCtrl maskt;
    AI_ByState aI_ByState;
    static SpriteRenderer spriteRenderer;

    Animator animator;
    public Text text;


    private void Awake()
    {
        rigibody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        MoveSpeed = 5f;

        objectCount = 0;
        playerT = GetplayerT();
        maskt = MaskCtrl.GetMaskT();
        aI_ByState = AI_ByState.GetAi();
        playerT.objectCountChange += aI_ByState.ChangeImage;
        playerT.objectCountChange += maskt.ChangeMaskD;
        playerT.objectCountChange += maskt.ChangeMaskDistance;//這三行訂閱ObjectCount狀態有沒有發生改變
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))//按Q計數+1模擬吃到東西
        {
            Debug.Log("Q");
            playerT.ObjectCount = playerT.objectCount + 1;
            text.text = "遮罩等級"+playerT.objectCount;
        }


        if (Input.GetKeyDown(KeyCode.W))//按Q計數+1模擬吃到東西
        {
            Debug.Log("Q");
            playerT.ObjectCount = playerT.objectCount -1;
            text.text = "遮罩等級" + playerT.objectCount;
        }

        //................
        //if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) { animator.SetBool("IsRun", true); }
        if (Input.GetKey(KeyCode.Z) && PlayerIsMove()&& MaskCtrl.MaskLimit > 0.01)
        {
            //fox_T.active = false;
            MoveSpeed = 5;//10
            PlayerIsRun = true;
            animator.SetBool("IsRun",true);
        }
        else if (PlayerIsMove()&&MaskCtrl.MaskLimit>0.01)
        {
            //fox_T.active = false;
            MoveSpeed = 2;//5
            PlayerIsRun = false;
            animator.SetBool("IsRun",true);
        }
        else {
            //fox_T.active = true;
            MoveSpeed = 0;
            animator.SetBool("IsRun",false);
        }

        PlayerPosition = transform.position;
        float moveX = 0f;
        float moveY = 0f;
        if (Input.GetKey(KeyCode.UpArrow)) moveY = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) moveY = -1f;
        if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;
        moveDir = new Vector3(moveX, moveY).normalized;
    }
    public static bool PlayerIsMove() {
        bool returnbool;
        if (Input.GetKey(KeyCode.LeftArrow)) {

            spriteRenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {

            spriteRenderer.flipX = true;
        } 

<<<<<<< Updated upstream
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
=======
        if (Input.GetKeyDown(KeyCode.Space))
>>>>>>> Stashed changes
        {
            returnbool = true;
        }
        else {
            returnbool = false;
        }

        return returnbool;
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
            if (value >= 0 && value < 4)//目前陣列最高是5個，濾掉不符合的值
            {
                Debug.Log("C++");
                Debug.Log("value" + value);
                objectCount = value;
                PlayerEventArgs playerEventArgs = new PlayerEventArgs();
                playerEventArgs.ObjectCount = objectCount;
                objectCountChange.Invoke(this, playerEventArgs);//狀態有發生變化，傳遞變化後的狀態給訂閱者
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
<<<<<<< Updated upstream




    public static PlayerCtrl GetplayerT()
    {
        if (playerT == null)
        {
            return playerT = new PlayerCtrl();
        }
        else
        {
            return playerT;
        }
    }

    public static void LevelUp() {
=======
    public   void LevelUp() {
>>>>>>> Stashed changes
        playerT.ObjectCount += 1;
    }
}


public delegate void ObjectCountChangeHandler(PlayerCtrl playerT, PlayerEventArgs e);
public class PlayerEventArgs : EventArgs
{
    public int ObjectCount { get; set; }
}

