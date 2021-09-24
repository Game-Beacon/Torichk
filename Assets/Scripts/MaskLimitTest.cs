using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MaskLimitTest : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        //text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float f = 0.01f;
        if (Input.GetKey(KeyCode.X) && MaskCtrl.MaskLimit>= 0+f)
        {
            MaskCtrl.MaskLimit -= f;

        }
        if (Input.GetKey(KeyCode.C) && MaskCtrl.MaskLimit <1)
        {
            MaskCtrl.MaskLimit += f;
        }
        text.text = "體力:"+(MaskCtrl.MaskLimit*100).ToString("0")+"/100";
    }
}
