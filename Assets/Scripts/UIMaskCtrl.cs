using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaskCtrl : MonoBehaviour
{
    float[] time = new float[] {5,0.2f,1,0.2f,0.8f,1f };
    float[] time_end = new float[6];
    public GameObject viewMask;
    // Start is called before the first frame update
    void Start()
    {
        Settime();
        //InvokeRepeating("Settime",0,18.7f);
    }

    // Update is called once per frame
    void Update()
    {
        Around();
    }


    void Around() {
        Debug.Log("aaa");
        if (Time.time>=time_end[0])
        {
            if (Time.time>=time_end[1])
            {
                if (Time.time>=time_end[2])
                {
                    if (Time.time>=time_end[3])
                    {

                        if (Time.time>=time_end[4])
                        {
                            if (Time.time>= time_end[5])
                            {
                                MaskBase();
                                Settime();
                                return;
                            }
                            Mask50();
                            return;
                        }
                        MaskBase();
                        return;
                    }
                    Mask50();
                    return;
                }
                MaskBase();
                return;
            }
            Mask50();
            return;
        }
        MaskBase();
        return;
    }

    void Settime() {
        time_end[0] = Time.time+time[0];

        for (int i = 1; i < time.Length; i++)
        {
            time_end[i] = time_end[i-1]+time[i];
        }
    }

    void MaskBase() {
        viewMask.transform.localScale = new Vector3(4,4,0);
    }

    void Mask50() {
        viewMask.transform.localScale = new Vector3(50,50,0);
    }

}
