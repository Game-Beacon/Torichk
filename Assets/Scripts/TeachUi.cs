using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;
using  UnityEngine.SceneManagement;
public class TeachUi : MonoBehaviour
{
        public Sprite[] _sprites; 
        private Image _image;
        private int count = 0;

        public static bool CanTeach = true;
        // Start is called before the first frame update
        void Start()
        {
            _image = GetComponent<Image>();
            _image.sprite = _sprites[0];
        }
    
        // Update is called once per frame
        void Update()
        {
    
    
            if (Input.anyKeyDown&&count<_sprites.Length+1)
            {
                count++;
                if (count>_sprites.Length)
                {
                    CanTeach = false;
                    SceneManager.LoadScene("m1");
                }
                else
                {
                    _image.sprite = _sprites[count-1];
                AkSoundEngine.PostEvent("Page_turn", gameObject);
            }
            
        }
        }
}
