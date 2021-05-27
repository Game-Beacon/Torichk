using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerT : MonoBehaviour
{

    //private static PlayerT playerT;
    //MaskT maskt;
    private void Start()
    {
        //objectCount = 0;
        //playerT = GetplayerT();
        //maskt = MaskT.GetMaskT();
        //playerT.objectCountChange += playerT.ChangeImage;
        //playerT.objectCountChange += maskt.ChangeMaskD;
        //playerT.objectCountChange += maskt.ChangeMaskDistance;//這三行訂閱ObjectCount狀態有沒有發生改變

    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))//按Q計數+1模擬吃到東西
        //{
        //    Debug.Log("Q");
        //    playerT.ObjectCount = playerT.objectCount+1;
        //}
    }



    //private int objectCount;

    //public int ObjectCount
    //{
    //    get { return objectCount; }
    //    set {
    //        if (value>=0&&value<5)//目前陣列最高是5個，濾掉不符合的值
    //        {
    //            Debug.Log("C++");
    //            Debug.Log("value"+value);
    //            objectCount = value;
    //            PlayerEventArgs playerEventArgs = new PlayerEventArgs();
    //            playerEventArgs.ObjectCount = objectCount;
    //            this.objectCountChange.Invoke(this,playerEventArgs);//狀態有發生變化，傳遞變化後的狀態給訂閱者
    //        }
    //    }
    //}

    //private ObjectCountChangeHandler objectCountChange; 
        
    //  public event ObjectCountChangeHandler  Change {
    //    add {
    //        this.objectCountChange += value;
    //    }
    //    remove {
    //        this.objectCountChange -= value;
    //    }
    //}


    //internal void ChangeImage(PlayerT playerT, PlayerEventArgs e)
    //{
    //    if (e.ObjectCount ==4)
    //    {
    //        Debug.Log("ChangeToImage2");
    //    }
    //}

    //public static PlayerT GetplayerT()
    //{
    //    if (playerT == null)
    //    {
    //        return playerT = new PlayerT();
    //    }
    //    else
    //    {
    //        return playerT;
    //    }
    //}
}
//public delegate void ObjectCountChangeHandler(PlayerT playerT,PlayerEventArgs e);
//public class PlayerEventArgs : EventArgs {
//    public int ObjectCount { get; set; }
//}