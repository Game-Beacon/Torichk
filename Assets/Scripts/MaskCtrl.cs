using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskCtrl : MonoBehaviour
{
    #region 欄位
    public GameObject viewMask;
    public static float currectScal;
    //float maskChangePercentageCurrect;
    static MaskCtrl maskT;
    static float CurrectTargetScal;
    float CurrectMaskPercentage;//遮罩速度
    float[] TargetScal = new float[] { 1, 1, 1.5f, 2.5f, 4.5f };
    float[] MaskPercentage = new float[] {0.01f, 0.02f, 0.03f, 0.05f, 0.08f };//遮罩速度1.2.3.5.8

    #endregion

    public static MaskCtrl GetMaskT()
    {
        return maskT ?? new MaskCtrl();
    }

    private void Awake()
    {
        GetMaskT();
        CurrectMaskPercentage = MaskPercentage[0];
        CurrectTargetScal = TargetScal[0];

    }
    void Update()
    {
        Debug.Log("UpdateTasrget"+CurrectTargetScal);
        currectScal = MaskChangeFromAtoB(
            currectScal,                //A
            PlayerCtrl.PlayerIsMove() ? CurrectTargetScal:0//B mask 目標範圍，移動:往最大,不動:往0
            , PlayerCtrl.PlayerIsRun ? (CurrectMaskPercentage * 5) : (CurrectMaskPercentage));     //mask變化比例
        viewMask.transform.localScale = new Vector3(currectScal,currectScal,currectScal);          
    }

   float MaskChangeFromAtoB(float a,float b ,float changePercentage) {
        if (a > b * 0.99 && b!=0)   {return b;}
        if (b==0 && a<0.1)          {return 0;}
        return a + (b - a) * changePercentage;
    }

    internal void ChangeMaskD(PlayerCtrl playerT, PlayerEventArgs e)
    {
        //PlayerEventArgs playerEventArgs = e as PlayerEventArgs;
        CurrectMaskPercentage = MaskPercentage[e.ObjectCount];
        Debug.Log("CurrectMaskPercentage:" + CurrectMaskPercentage);
    }

    internal void ChangeMaskDistance(PlayerCtrl playerT, PlayerEventArgs e)
    {
        //PlayerEventArgs playerEventArgs = e as PlayerEventArgs;
        CurrectTargetScal = TargetScal[e.ObjectCount];
        Debug.Log("TargetScal" + TargetScal[e.ObjectCount]);
    }
}






