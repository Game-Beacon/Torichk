using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    static Vector3 PlayerPosition;
    public static Vector3 GetPlayerPosition()
    {
        return PlayerPosition;
    }

    //[SerializeField] private FieldOfView_2 fieldOfView;
    public float MOVE_SPEED = 60f;
    private Rigidbody2D rigibody2D;
    private Vector3 moveDir;
    public static bool Hp = true;


    public Image image;
    public GameObject dark;
    float fix = 100;
    float[] Maskgear = new float[] {0,1,1.5f,2.5f,4.5f};
    float[] Vgear = new float[] {0,2,3.25f,4.75f,7.75f};
    int count = 1;



    private void Awake()
    {
        rigibody2D = GetComponent<Rigidbody2D>();

    }
    // Update is called once per frame
    void Update()
    {
        PlayerPosition = transform.position;
        //fieldOfView.SetAimDirection(moveDir);
        //fieldOfView.SetOrigin(transform.position);
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.UpArrow)) {
            moveY = 1f;

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveY = -1f;

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f;

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveX = 1f;

        }

        moveDir = new Vector3(moveX, moveY).normalized;

        MaskCtrl();

    }

    void MaskCtrl() {

        if(Input.GetKeyDown(KeyCode.Z) && count < Maskgear.Length-1)//up
        {
            if (count == 0) { StopCoroutine("timer3"); }
            count++;
            image.rectTransform.sizeDelta = new Vector2(fix* Maskgear[count], fix * Maskgear[count]);
            MOVE_SPEED = Vgear[count];

        }

        if(Input.GetKeyDown(KeyCode.X) && count >0)//down
        {
            count--;
            image.rectTransform.sizeDelta = new Vector2(fix * Maskgear[count], fix * Maskgear[count]);
            MOVE_SPEED = Vgear[count];
            if (count == 0) {
                StartCoroutine("timer3");
                
                Debug.Log("C");
            }
        }



    }


    //int timerCount = 0;
    IEnumerator timer3() {

        for (int i = 0; i < 4; i++)
        {
            image.rectTransform.sizeDelta = new Vector2(fix * Maskgear[0], fix * Maskgear[0]);
            yield return new WaitForSeconds(1);
            image.rectTransform.sizeDelta = new Vector2(fix * Maskgear[1], fix * Maskgear[1]);
            yield return new WaitForSeconds(0.5f);
            image.rectTransform.sizeDelta = new Vector2(fix * Maskgear[0], fix * Maskgear[0]);
            yield return new WaitForSeconds(0.2f);
        }
            yield return null;

    }


    private void FixedUpdate()
    {
        rigibody2D.velocity = moveDir*MOVE_SPEED;
    }


}

