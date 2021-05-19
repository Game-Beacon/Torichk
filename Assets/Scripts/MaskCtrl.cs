using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MaskCtrl : MonoBehaviour
{

    public GameObject _mask;
    float target;
    float currect;
    float changePercentage;
    float maskSpeed;


    private void Awake()
    {
        target = 1;
        currect = 0;
        changePercentage = 0.01f;
        maskSpeed = 0.003f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)&&target<5)
        {
            Debug.Log("MaskLevelUP");
          target++;
        }

        if (Input.GetKeyDown(KeyCode.S) && target > 1)
        {
            Debug.Log("MaskLevelDown");
            target--;
        }

        changePercentage =PlayerCtrl.PlayerIsRun ? maskSpeed*5:maskSpeed;
        if (Input.GetKey(KeyCode.UpArrow)|| Input.GetKey(KeyCode.DownArrow)|| Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.LeftArrow))
        {
            currect = MaskChangeFromAtoB(currect, target,changePercentage);
        }
        else
        {
            currect = MaskChangeFromAtoB(currect, 0,changePercentage);
        }
        _mask.transform.localScale = new Vector3(currect,currect,currect);      
    }
   float MaskChangeFromAtoB(float a,float b ,float changePercentage) {
        if (a > b * 0.99 && b!=0)   {return b;}
        if (b==0 && a<0.1)          {return 0;}

        return a + (b - a) * changePercentage;
    }


    void DrawCircle(float r) {
        Handles.color = Color.red;
        Handles.DrawWireArc(_mask.transform.position, Vector3.up, Vector3.forward, 360, r);
    }

}






