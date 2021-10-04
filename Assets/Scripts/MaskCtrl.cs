using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskCtrl : MonoBehaviour
{
    #region 欄位
    public GameObject viewMask;
    public static float currectScal;
    public static float CurrectTargetScal;
    float CurrectMaskPercentage;//遮罩速度
    float[] TargetScal = new float[] { 1f, 2f, 3f, 10.5f,};
    float[] MaskPercentage = new float[] {0.03f, 0.03f, 0.03f, 0.03f };//遮罩速度1.2.3.5.8
    public static float MaskLimit;//0 to 1.00
    #endregion
    private void Awake()
    {
        CurrectMaskPercentage = MaskPercentage[0];
        CurrectTargetScal = TargetScal[0];
        MaskLimit = 1;
        currectScal = 1.0f;
        WaitEnd = 0;
        MaskWait(5,CurrectTargetScal);
        CanTo0 = true;
    }

    public  static bool CanTo0;
    void Update()
    {
        if (Time.time<= WaitEnd&&!PlayerCtrl.PlayerIsMove)
        {
            viewMask.transform.localScale = new Vector3(WaitScal,WaitScal,WaitScal);
        }
        else
        {

            currectScal = MaskChangeFromAtoB(
                    currectScal,                //A
                    (PlayerCtrl.PlayerIsMove&&!PlayerCtrl.PlayerIsRun) ? CurrectTargetScal*MaskLimit:0//B mask 目標範圍，移動:往最大,不動:往0
                    , PlayerCtrl.PlayerIsRun ? (0.002f) : (CurrectMaskPercentage));     //mask變化比例
                viewMask.transform.localScale = new Vector3(currectScal,currectScal,currectScal);
                if (!PlayerCtrl.PlayerIsMove)
                {
                    PlayerCtrl.Player.animator.SetBool("IsRun",false);
                    if (currectScal<0.1f&&CanTo0)
                    {
                        PlayerCtrl.Player.animator.SetBool("CanD",true);
                        CanTo0 = false;
                    }

                }
        }
        //MaskL();
        if (Input.GetKeyUp(KeyCode.RightArrow)||Input.GetKeyUp(KeyCode.LeftArrow)||Input.GetKeyUp(KeyCode.UpArrow)||Input.GetKeyUp(KeyCode.DownArrow))
        {
            MaskWait(5,currectScal);
        }
    }
    
    static float WaitScal;
     static float WaitEnd;
    public static void MaskWait(float s,float Masktarget)
    {
        WaitScal = Masktarget;
        WaitEnd =Time.time+ s;
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






