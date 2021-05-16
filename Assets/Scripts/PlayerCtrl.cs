using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    public GameObject s;

    static float MaskDistance;
    static Vector3 PlayerPosition;
    Vector3 currectScale;
    Vector3 TargetMaskScale ;
    float MaskV;
    bool isRun ;
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
        currectScale = new Vector3(1,1,1);
        MOVE_SPEED = 5f;
        MaskV = 10f;
        TargetMaskScale = new Vector3(2, 2, 2);
        isRun = false;
    }
    void Update()
    {


        if (Input.GetKey(KeyCode.Z))
        {
            MaskV = 0.1f;
            MOVE_SPEED = 10;
        }
        else
        {
            MaskV = 10f;
            MOVE_SPEED = 5;
        }

        PlayerPosition = transform.position;

        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.UpArrow)) {
            moveY = 1f;
            StartCoroutine(MaskChange(MaskV,currectScale,TargetMaskScale));
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            moveY = -1f;
            StartCoroutine(MaskChange(MaskV,currectScale,TargetMaskScale));
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            moveX = -1f;
            StartCoroutine(MaskChange(MaskV,currectScale, TargetMaskScale));
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            moveX = 1f;
            StartCoroutine(MaskChange(MaskV,currectScale,TargetMaskScale));
        }

        if (!(Input.GetKey(KeyCode.UpArrow)|| Input.GetKey(KeyCode.DownArrow)|| Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.RightArrow)))
        {
            StartCoroutine(UnMaskChange(MaskV, currectScale,new Vector3(0,0,0)));
        }

        moveDir = new Vector3(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        rigibody2D.velocity = moveDir*MOVE_SPEED;
    }

    IEnumerator MaskChange(float duration, Vector3 Start, Vector3 End)
    {
        var timeStart = Time.time;
        var timeEnd = timeStart + duration;
        while (Time.time < timeEnd)
        {
            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                currectScale = s.transform.localScale;
                yield break;
            }


            var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
            var v = t;
            var scale = Vector3.LerpUnclamped(Start, End, v);
            //this.transform.localPosition = position;
            s.transform.localScale = scale;
            yield return null;
        }
        s.transform.localScale = End;
        //this.transform.localPosition = posEnd;
    }

    IEnumerator UnMaskChange(float duration, Vector3 Start, Vector3 End)
    {
        var timeStart = Time.time;
        var timeEnd = timeStart + duration;
        while (Time.time < timeEnd)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                currectScale = s.transform.localScale;
                yield break;
            }

            var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
            var v = t;
            var scale = Vector3.LerpUnclamped(Start, End, v);
            //this.transform.localPosition = position;
            s.transform.localScale = scale;
            yield return null;
        }
        s.transform.localScale = End;
        //this.transform.localPosition = posEnd;
    }

}

