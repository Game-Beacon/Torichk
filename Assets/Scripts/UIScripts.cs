using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;
public class UIScripts : MonoBehaviour
{
    public GameData _gameData;
    private string[] m = new[] {"m1", "m3","m2"};
    // Start is called before the first frame update

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoSc()
    {
        SceneManager.LoadScene(m[(int)_gameData._uiTitle]);
    }
}
