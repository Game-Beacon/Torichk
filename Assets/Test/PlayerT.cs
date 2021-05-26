using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerT : MonoBehaviour
{
    public Image image1;
    public Image image2;
    public Image currectImage;
    private static PlayerT playerT;


    private void Start()
    {
        currectImage = image1;
        objectCount = 0;
        playerT = new PlayerT();
        //GetplayerT().OnObjectCountIsChange += playerT.Action;
        //GetplayerT().OnObjectCountIsChange += MaskT.GetMaskT().ChangeMaskDistance;
        //GetplayerT().OnObjectCountIsChange += MaskT.GetMaskT().ChangeMaskD;//這三行訂閱ObjectCount狀態有沒有發生改變

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))//按Q計數+1模擬吃到東西
        {
            Debug.Log("Q");
            playerT.ObjectCount = playerT.objectCount+1;
        }
    }



    private int objectCount;
    PlayerEventArgs playerEventArgs = new PlayerEventArgs();
    public int ObjectCount
    {
        get { return objectCount; }
        set {
            if (value>=0&&value<5)//目前陣列最高是5個，濾掉不符合的值
            {
                Debug.Log("C++");
                Debug.Log("value"+value);
                objectCount = value;
                playerEventArgs.ObjectCount = objectCount;

                GetplayerT().OnObjectCountIsChange += playerT.Action;
                playerT.OnObjectCountIsChange.Invoke(playerT, playerEventArgs);
                GetplayerT().OnObjectCountIsChange += MaskT.GetMaskT().ChangeMaskDistance;
                playerT.OnObjectCountIsChange.Invoke(playerT, playerEventArgs);
                GetplayerT().OnObjectCountIsChange += MaskT.GetMaskT().ChangeMaskD;
                playerT.OnObjectCountIsChange.Invoke(playerT,playerEventArgs);//狀態有發生變化，傳遞變化後的狀態給訂閱者
            }
        }
    }

    public event EventHandler OnObjectCountIsChange;

    internal void Action(object sender, EventArgs e)
    {
        PlayerEventArgs pEventarges = e as PlayerEventArgs;

        if (pEventarges.ObjectCount ==4)
        {
            currectImage = image2;
            Debug.Log("ChangeToImage2");
        }
    }

    public static PlayerT GetplayerT()
    {
        if (playerT == null)
        {
            return playerT = new PlayerT();
        }
        else
        {
            return playerT;
        }
    }
}

public class PlayerEventArgs : EventArgs {
    public int ObjectCount { get; set; }
}