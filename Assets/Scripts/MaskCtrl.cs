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
    float MaskLevel;

    private void Awake()
    {
        target = 1;
        currect = 0;
        changePercentage = 0.01f;
        MaskLevel = 1.0f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)&&target<5)
        {
          target = MaskLevel++;
        }

        changePercentage =PlayerCtrl.PlayerIsRun ? 0.1f:0.01f;
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
        return a + (b - a) * changePercentage;
    }


    void DrawCircle(float r) {
        Handles.color = Color.red;
        Handles.DrawWireArc(_mask.transform.position, Vector3.up, Vector3.forward, 360, r);
    }

}






