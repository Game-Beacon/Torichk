using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskCtrl : MonoBehaviour
{

    public GameObject _mask;
    float target;
    float currect;
    float changePercentage;

    private void Awake()
    {
        target = 10;
        currect = 0;
        changePercentage = 0.01f;
    }
    void Update()
    {
        changePercentage =PlayerCtrl.PlayerIsRun ? 0.01f:0.001f;
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

}






