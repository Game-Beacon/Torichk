using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseEventPost : MonoBehaviour
{
    public AK.Wwise.Event MyEvent;
    // Start is called before the first frame update
    public void AnimationEvent()
    {
        MyEvent.Post(gameObject);
    }
    public AK.Wwise.Event MyEvent2;
    // Start is called before the first frame update
    public void AnimationEvent2()
    {
        MyEvent2.Post(gameObject);
    }
    public AK.Wwise.Event MyEvent3;
    // Start is called before the first frame update
    public void AnimationEvent3()
    {
        MyEvent3.Post(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
