using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskCtrl : MonoBehaviour
{

    public GameObject g;
    float t = 10;
    float c = 0;

    void Start()
    {

    }

    void Update()
    {
        if (Input.anyKey)
        {
            c = MaskChange(c, t, 0.01f);
            Debug.Log(c);
        }
        else
        {
            c = MaskChange(c, 0, 0.01f);
            Debug.Log(c);
        }

        // c = MaskChange(c,Input.GetKey(KeyCode.Space)? t:0, 0.01f);
        g.transform.localScale = new Vector3(c,c,c);      
    }
   float MaskChange(float a,float b ,float k) {
        return a + (b - a) * k;
    }

}






