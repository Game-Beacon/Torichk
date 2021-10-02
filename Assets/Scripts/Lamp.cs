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
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        BeUse = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag =="Player")
        {
            
            // _animator.SetBool("BeTouch",true);
            if (this.name.ToLower().Contains("Crystal".ToLower()) && BeUse) 
            {
                //UseLamp();
            }
            else
            {
                _animator.SetBool("BeTouch",true);
                other.GetComponent<PlayerCtrl>().KillPlayer();
            }

            BeUse = false;
        }
        //停止音效
    }

    public  void UseLamp()
    {
        if (this.name.ToLower().Contains("Crystal".ToLower()) && BeUse)
        {
            PlayerCtrl.Player.LevelUp();
            _animator.SetBool("LampBeTouch",true);
            ChangeMap.LampBeUse = true;
            MaskCtrl.MaskWait(5,MaskCtrl.CurrectTargetScal);
            if (SceneManager.GetActiveScene().name =="m1")
            {
                var map = GameObject.Find("MapCreat");
                map.GetComponent<MapV2>().ChangeSigelState();
            }
        }

    }

}
