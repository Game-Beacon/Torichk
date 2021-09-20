using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;
public class MainUI : MonoBehaviour
{
    private Material _material;
    private bool Stop = true; 
    public Image _image;
    public Sprite[] _Sprites;
    public GameData _gameData;
    void Start ()
    {
        _material = GetComponent<Image>().material;
        StartCoroutine(Tween(0.5f, 0, 1));
        _image.sprite = _Sprites[(int)_gameData._uiTitle];
    }

    private void Update()
    {
        _image.sprite = _Sprites[(int)_gameData._uiTitle];
    }
    
    IEnumerator Tween(float duration, float Start, float End)
    {
        Debug.Log("start");
        while (Stop)
        {
            yield return new WaitForSeconds(16);//15+1秒一般
            /////////////////////////////////////////////////////
            /*var timeStart = Time.time;
            var timeEnd = timeStart + 0.2f;
            while (Time.time < timeEnd)
            {
                var t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
                var v = ((t = t - 1) * t * t + 1);
                var target = Mathf.LerpUnclamped(Start, End, v);
                _material.SetFloat("_Float", target);
                yield return null;
            }*/
            _material.SetFloat("_Float",1);
            yield return new WaitForSeconds(0.2f);//0.2秒亮
            //////////////////////////////////////////////////////
            var timeStart2 = Time.time;
            var timeEnd2 = timeStart2 + 0.5f;
            while (Time.time < timeEnd2)
            {
                var t = Mathf.InverseLerp(timeStart2, timeEnd2, Time.time);
                var v = ((t = t - 1) * t * t + 1);
                var target = Mathf.LerpUnclamped(End, Start, v);
                _material.SetFloat("_Float", target);
                yield return null;
            }//0.5秒漸暗
            yield return new WaitForSeconds(1f);//1秒暗
            /////////////////////////////////////////////////////////
            /*var timeStart3 = Time.time;
            var timeEnd3 = timeStart3 + 0.2f;
            while (Time.time < timeEnd3)
            {
                var t = Mathf.InverseLerp(timeStart3, timeEnd3, Time.time);
                var v = ((t = t - 1) * t * t + 1);
                var target = Mathf.LerpUnclamped(Start, End, v);
                _material.SetFloat("_Float", target);
                yield return null;
            }*/
            _material.SetFloat("_Float",1);
            yield return new WaitForSeconds(0.2f);//0.2秒亮
            ////////////////////////////////////////
            var timeStart4 = Time.time;
            var timeEnd4 = timeStart4 + 0.5f;
            while (Time.time < timeEnd4)
            {
                var t = Mathf.InverseLerp(timeStart4, timeEnd4, Time.time);
                var v = ((t = t - 1) * t * t + 1);
                var target = Mathf.LerpUnclamped(End, Start, v);
                _material.SetFloat("_Float", target);
                yield return null;
            }//0.5秒漸暗
            ////////////////////////////////////////////////
            yield return new WaitForSeconds(0.8f);//0.8秒暗
            _material.SetFloat("_Float",1);//亮
            yield return new WaitForSeconds(0.8f);//0.5秒
            ////////////////////////////////////////////////

            var timeStart5 = Time.time;
            var timeEnd5 = timeStart5 + duration;
            while (Time.time < timeEnd5)
            {
                var t = Mathf.InverseLerp(timeStart5, timeEnd5, Time.time);
                var v = ((t = t - 1) * t * t + 1);
                var target = Mathf.LerpUnclamped(End, Start, v);
                _material.SetFloat("_Float", target);
                yield return null;
            }//0.8秒漸暗

        }
    }
}
