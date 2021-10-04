using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIText : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject _gameObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private bool pointerDown;
    public void OnPointerEnter(PointerEventData eventData)
    {
        _gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _gameObject.SetActive(false);
    }
}
