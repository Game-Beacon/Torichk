using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Lamp : MonoBehaviour
{
    //public AK.Wwise.Event MyEvent;
    private Animator _animator;
    public GameData _gameData;
    private float timeEnd;
    private bool BeUse;

    private bool AniLock;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        BeUse = true;
        AniLock = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag =="Player"&& BeUse)
        {
            if ((!name.ToLower().Contains("Crystal".ToLower()))&&AniLock )//&&Vector2.Distance(other.transform.position,transform.position)<0.5 ) 
            {
                PlayerCtrl.CanMove = false;
                if (!name.ToLower().Contains("moundsmoke".ToLower()))
                {
                    //_animator.SetBool("BeTouch",true);
                    _animator.Play("Attack");
                }
                PlayerCtrl.Player.KillPlayer();
                //other.GetComponent<PlayerCtrl>().KillPlayer();
                BeUse = false;
                AniLock = false;
            }
        }
        //停止音效
    }

    public  void UseLamp()
    {
        if (this.name.ToLower().Contains("Crystal".ToLower()) && BeUse)
        {
            PlayerCtrl.Player.LevelUp();
            //_animator.SetBool("LampBeTouch",true);
            _animator.Play("Crystal_Crash");
            ChangeMap.LampBeUse = true;
            MaskCtrl.MaskWait(5,MaskCtrl.CurrectTargetScal);
            if (SceneManager.GetActiveScene().name =="m1")
            {
                var map = GameObject.Find("MapCreat");
                map.GetComponent<MapV2>().ChangeSigelState();
            }

            BeUse = false;
        }

    }

}
