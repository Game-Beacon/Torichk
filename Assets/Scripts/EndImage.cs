using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  UnityEngine.SceneManagement;
public class EndImage : MonoBehaviour
{
    public Sprite[] _sprites;
    private Image _image;
    public GameData gameData;
    //private string[] m = new[] {"", "m3","m2"};
    private void Start()
    {
        _image = GetComponent<Image>();
        _image.sprite = _sprites[(int)gameData._uiTitle-1];
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            
            SceneManager.LoadScene("UI");
            //SceneManager.LoadScene(m[(int)gameData._uiTitle]);
        }
    }
}
