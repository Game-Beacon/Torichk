using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ButtonAni : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private bool pointerDown;


    public void OnPointerEnter(PointerEventData eventData)
    {        
        _animator.SetBool("Exit",false);
        _animator.SetBool("Enter",true);
        Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {               
        _animator.SetBool("Enter",false);
        _animator.SetBool("Exit",true);
        Debug.Log("Exit");
    }
}
