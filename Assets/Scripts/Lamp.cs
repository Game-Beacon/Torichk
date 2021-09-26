using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            
            _animator.SetBool("BeTouch",true);
            if (this.name.ToLower().Contains("Crystal".ToLower()) && BeUse == true) 
            {
                if (SceneManager.GetActiveScene().name =="m3"||SceneManager.GetActiveScene().name =="m2")
                {
                    ChangeMap.LampBeUse = true;
                }
                other.GetComponent<PlayerCtrl>().LevelUp();
            }
            else
            { 
                other.GetComponent<PlayerCtrl>().KillPlayer();
            }

            BeUse = false;
        }
        //停止音效
    }

}
