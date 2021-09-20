using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    Animator animator;
    bool IsDie;
    // Start is called before the first frame update
    void Start()
    {
        IsDie = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"&& IsDie ==false)
        {
            PlayerCtrl.LevelUp();
            animator.SetBool("Playertouch",true);
            IsDie = true;
            //animator.SetBool("Playertouch",false);

        }
    }
}
