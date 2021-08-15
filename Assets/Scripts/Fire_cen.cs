using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_cen : MonoBehaviour
{
    SpriteRenderer Fire_spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
       Fire_spriteRenderer =  GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position =PlayerCtrl.PlayerPosition + new Vector3(-1,0.5f,-0.1f);
            Fire_spriteRenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position =PlayerCtrl.PlayerPosition + new Vector3(1, 0.5f, -0.1f);
            Fire_spriteRenderer.flipX = true;
        }
    }
}
