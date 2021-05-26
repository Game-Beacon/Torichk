using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskT : MonoBehaviour
{
    static MaskT maskT;
    float CurrectMaskDistance;
    float CurrectMaskD;
    float[] MaskDistance = new float[] { 0, 1, 1.5f, 2.5f, 4.5f };
    float[] MaskD = new float[] { 1, 2, 3, 5, 8 };

    public static MaskT GetMaskT() {
        return maskT == null ? new MaskT():maskT;
    }

    private void Start()
    {
        CurrectMaskD = MaskD[0];
        CurrectMaskDistance = MaskDistance[0];
    }

    internal void ChangeMaskD(object sender, EventArgs e)
    {
        PlayerEventArgs playerEventArgs = e as PlayerEventArgs;
        CurrectMaskD = MaskD[playerEventArgs.ObjectCount];
        Debug.Log("MaskD:"+CurrectMaskD);
    }

    internal void ChangeMaskDistance(object sender, EventArgs e)
    {
        PlayerEventArgs playerEventArgs = e as PlayerEventArgs;
        CurrectMaskDistance = MaskDistance[playerEventArgs.ObjectCount];
        Debug.Log("MaskDistance"+CurrectMaskDistance);
    }
}
