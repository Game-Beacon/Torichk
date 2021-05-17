using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    static float MaskDistance;
    static Vector3 PlayerPosition;
    public static Vector3 GetPlayerPosition()
    {
        return PlayerPosition;
    }
    public static float GetMaskDistance() {
        return MaskDistance;
    }
    public float MOVE_SPEED;
    private Rigidbody2D rigibody2D;
    private Vector3 moveDir;
    public static bool Isdeath = false;

    private void Awake()
    {
        rigibody2D = GetComponent<Rigidbody2D>();
        MOVE_SPEED = 5f;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            MOVE_SPEED = 10;
        }
        else
        {
            MOVE_SPEED = 5;
        }

        PlayerPosition = transform.position;

        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.UpArrow)) {
            moveY = 1f;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            moveX = 1f;
        }
        moveDir = new Vector3(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        rigibody2D.velocity = moveDir*MOVE_SPEED;
    }
}

