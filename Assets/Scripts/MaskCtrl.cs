using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskCtrl : MonoBehaviour
{
    #region 欄位
    public GameObject viewMask;
    public float[] MaskLevel;
    float targetScal;
    float currectScal;
    float MaskchangePercentage;
    float maskChangePercentageBase;
    int MaskLevelCount;
    #endregion


    private void Awake()
    {
        targetScal = 1;
        currectScal = 0;
        MaskchangePercentage = 0.03f;
        maskChangePercentageBase = 0.003f;
        MaskLevel = new float[5] { 1,2,3,5,7};
        MaskLevelCount = 0;
    }
    void Update()
    {
        MaskLevelupOrDown();
        targetScal = MaskLevel[MaskLevelCount];
        MaskchangePercentage = PlayerCtrl.PlayerIsRun ?  (maskChangePercentageBase*5) : (maskChangePercentageBase);//控制走跟跑時mask變化比例為5倍
        currectScal = MaskChangeFromAtoB(
            currectScal,                //A
            PlayerCtrl.PlayerIsMove() ? targetScal:0//B mask 目標範圍，移動:往最大,不動:往0
            ,MaskchangePercentage);     //mask變化比例
        viewMask.transform.localScale = new Vector3(currectScal,currectScal,currectScal);          
    }

   float MaskChangeFromAtoB(float a,float b ,float changePercentage) {
        if (a > b * 0.99 && b!=0)   {return b;}
        if (b==0 && a<0.1)          {return 0;}
        return a + (b - a) * changePercentage;
    }
    void MaskLevelupOrDown() {
        if (Input.GetKeyDown(KeyCode.A) && MaskLevelCount < MaskLevel.Length - 1)
        {
            Debug.Log("MaskLevelUP");
            MaskLevelCount++;
        }
        if (Input.GetKeyDown(KeyCode.S) && MaskLevelCount > 1)
        {
            Debug.Log("MaskLevelDown");
            MaskLevelCount--;
        }
    }
}






