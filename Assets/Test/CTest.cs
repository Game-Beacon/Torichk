using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTest : MonoBehaviour
{
    int timer = 0;
    bool b = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator timer3() {
        yield return new WaitForSeconds(1);
        timer++;
        b = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (b)
        {
            StartCoroutine("timer3");
            b = false;
            Debug.Log(timer);
        }
        
    }
}
