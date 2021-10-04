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
        public Sprite LoadSprite;
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
                if (count>=_sprites.Length)
                {
                    CanTeach = false;
                    if (SceneManager.GetActiveScene().name.ToLower().Contains("UI_Six".ToLower()))
                    {
                        //SceneManager.LoadScene(ChangeMap.gameData.Current);
                        _image.sprite = LoadSprite;
                        SceneManager.LoadScene("m1"); 

                    }
                    else
                    {                        
                        _image.sprite = LoadSprite;
                        SceneManager.LoadScene("m1"); 
                    }

                }
                else
                {
                    _image.sprite = _sprites[count];
                    AkSoundEngine.PostEvent("Page_turn", gameObject);
                }
            
        }
        }
}
