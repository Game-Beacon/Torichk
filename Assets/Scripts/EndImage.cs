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

    private float ChangeTime;
    //private string[] m = new[] {"", "m3","m2"};
    private void Start()
    {
        ChangeTime = Time.time + 5;
        _image = GetComponent<Image>();
        Debug.Log("End"+((int)gameData._uiTitle));
        _image.sprite = _sprites[((int)gameData._uiTitle)];
        switch (gameData._uiTitle)
        {
            case UiTitle.BadEnd:
                AkSoundEngine.PostEvent("Outro_B", gameObject);
                break;
            case UiTitle.GoodEnd:
                AkSoundEngine.PostEvent("Outro_G", gameObject);
                break;
        }
        
    }

    private void Update()
    {
        if (Input.anyKeyDown&&Time.time>ChangeTime)
        {
            
            SceneManager.LoadScene("UI");
            //SceneManager.LoadScene(m[(int)gameData._uiTitle]);
            AkSoundEngine.PostEvent("Outro_buttom", gameObject);
            
        }
    }
}
