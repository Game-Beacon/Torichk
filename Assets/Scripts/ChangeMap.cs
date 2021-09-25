using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;
public class ChangeMap : MonoBehaviour
{
    public string MapStr { get; set; }
    public UiTitle uiTitle= UiTitle.GoodEnd;
    public static GameData gameData;
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
        if (MapStr!= null)
        {
            gameData._uiTitle = uiTitle;
            SceneManager.LoadScene(MapStr);   
        }
    }

}
