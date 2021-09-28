using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskCtrl : MonoBehaviour
{
    #region 欄位
    public GameObject viewMask;
    public static float currectScal;
    static float CurrectTargetScal;
    float CurrectMaskPercentage;//遮罩速度
    float[] TargetScal = new float[] { 1.5f, 2.5f, 4.5f, 5.5f,};
    float[] MaskPercentage = new float[] {0.01f, 0.02f, 0.03f, 0.05f };//遮罩速度1.2.3.5.8
    public static float MaskLimit;//0 to 1.00
    #endregion
    private void Awake()
    {
        CurrectMaskPercentage = MaskPercentage[0];
        CurrectTargetScal = TargetScal[0];
        MaskLimit = 1;
        currectScal = 1.0f;
    }
    void Update()
    {
        MaskL();
        currectScal = MaskChangeFromAtoB(
            currectScal,                //A
            (PlayerCtrl.PlayerIsMove&&!PlayerCtrl.PlayerIsRun) ? CurrectTargetScal*MaskLimit:0//B mask 目標範圍，移動:往最大,不動:往0
            , PlayerCtrl.PlayerIsRun ? (CurrectMaskPercentage /2) : (CurrectMaskPercentage));     //mask變化比例
        viewMask.transform.localScale = new Vector3(currectScal,currectScal,currectScal);
    }

    void MaskL()
    {
        float f = 0.01f;
        if (PlayerCtrl.PlayerIsMove && MaskLimit<= 1f)
        {
            MaskLimit += f;
        }else if(MaskLimit>0){
            MaskLimit -= f;
        }
    }


    float MaskChangeFromAtoB(float a,float b ,float changePercentage) {
        if (a > b * 0.99 && b!=0)   {return b;}
        if (b==0 && a<0.1)          {return 0;}
        return a + (b - a) * changePercentage;
    }

    internal void ChangeMaskD(PlayerCtrl playerT, PlayerEventArgs e)
    {
        CurrectMaskPercentage = MaskPercentage[e.ObjectCount];
    }

    internal void ChangeMaskDistance(PlayerCtrl playerT, PlayerEventArgs e)
    {
        CurrectTargetScal = TargetScal[e.ObjectCount];
    }
}






