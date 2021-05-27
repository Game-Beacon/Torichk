using System;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public static float MaskDistance;
    public static Vector3 PlayerPosition;
    public float MoveSpeed;
    private Rigidbody2D rigibody2D;
    private Vector3 moveDir;
    public static bool Isdeath = false;
    public static bool PlayerIsRun;
    //..........................
    private static PlayerCtrl playerT;
    MaskCtrl maskt;
    AI_ByState aI_ByState;

    private void Awake()
    {
        rigibody2D = GetComponent<Rigidbody2D>();
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
        }
        //................
        if (Input.GetKey(KeyCode.Z)&&PlayerIsMove())
        {
            MoveSpeed = 5;//10
            PlayerIsRun = true;
        }
        else
        {
            MoveSpeed = 2;//5
            PlayerIsRun = false;
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
        return (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) ? true:false;
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
            if (value >= 0 && value < 5)//目前陣列最高是5個，濾掉不符合的值
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
}


public delegate void ObjectCountChangeHandler(PlayerCtrl playerT, PlayerEventArgs e);
public class PlayerEventArgs : EventArgs
{
    public int ObjectCount { get; set; }
}

