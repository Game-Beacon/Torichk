using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;
public class UIScripts : MonoBehaviour
{
    public GameData _gameData;
    private string[] m = new[] {"m1", "m3","m2"};

    public GameObject _gameObject;
    // Start is called before the first frame update

    public void ExitGame()
    {
        _gameData._uiTitle = UiTitle.Start;
        Application.Quit();
    }

    public void GoSc()
    {
        if (TeachUi.CanTeach)
        {
            SceneManager.LoadScene("TeachSC");
        }
        else
        {
            SceneManager.LoadScene(m[(int)_gameData._uiTitle]);
        }

    }

    public void CloseCredit()
    {
        _gameObject.SetActive(false);

    }

    public void OpenCredit()
    {
        _gameObject.SetActive(true);
    }
}
