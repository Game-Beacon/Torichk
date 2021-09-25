using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;
public class ChangeMap : MonoBehaviour
{
    public string MapStr { get; set; }
    public UiTitle _uiTitle= UiTitle.GoodEnd;
    public static GameData gameData;
    public static bool LampBeUse = false;
    private void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag =="Player")
        {
            GoMap();
        }
    }

    public void GoMap()
    {
        if (MapStr!= null&& !LampBeUse)
        {
            gameData._uiTitle = _uiTitle;
            SceneManager.LoadScene(MapStr);   
        }
        else
        {
            gameData._uiTitle = UiTitle.BadEnd;
            SceneManager.LoadScene(MapStr);
        }
    }

}
