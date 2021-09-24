using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag =="Player")
        {
            _animator.SetBool("BeTouch",true);
            other.GetComponent<PlayerCtrl>().LevelUp();
        }
        
    }
}
