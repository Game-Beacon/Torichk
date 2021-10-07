using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaybuttomSound : MonoBehaviour
{
    // Start is called before the first frame update
    public void onClick()
    {
        AkSoundEngine.PostEvent("Intro_buttom", gameObject);
    }
    public void onClick2()
    {
        AkSoundEngine.PostEvent("Intro_buttom2", gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
