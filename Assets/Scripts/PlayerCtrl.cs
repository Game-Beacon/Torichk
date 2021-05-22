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

    private void Awake()
    {
        rigibody2D = GetComponent<Rigidbody2D>();
        MoveSpeed = 5f;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Z)&&PlayerIsMove())
        {
            MoveSpeed = 10;
            PlayerIsRun = true;
        }
        else
        {
            MoveSpeed = 5;
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
}

