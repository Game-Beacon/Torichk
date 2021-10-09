using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Black : MonoBehaviour
{
    private Material _material;
    private bool Stop = true;
    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<Image>().material;
        _material.SetFloat("_Float",0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GoBlack()
    {
        StartCoroutine(Tween(0.5f, 0, 1));
    }

    IEnumerator Tween(float duration, float Start, float End)
    {
        // while (Stop)
        // {
            var timeStart = Time.time;
            var timeEnd = timeStart + 1.5f;
            while (Time.time < timeEnd)
            {
                var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
                var v = ((t = t - 1) * t * t + 1);
                var target = Mathf.LerpUnclamped(Start, End, v);
                _material.SetFloat("_Float", target);
                yield return null;
            }
            SceneManager.LoadScene("UI_Six");
            Debug.Log("End");
        // }
    }
}
