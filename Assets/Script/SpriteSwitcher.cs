using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // สำคัญ: ต้องมี namespace นี้เพื่อใช้งาน UI Image

public class SpriteSwitcher : MonoBehaviour
{
    public Sprite spriteW; 
    public Sprite spriteQ;
    public Sprite spriteE;

    private Image uiImage; 

    void Start()
    {
       
        uiImage = GetComponent<Image>();

       
        if (uiImage == null)
        {
            Debug.LogError("Unknow UI Image ");
            enabled = false; 
            return;
        }
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeSprite(spriteW);
        }
       
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeSprite(spriteQ); 
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeSprite(spriteE);
        }
    }

   
    void ChangeSprite(Sprite newSprite)
    {
        if (uiImage != null && newSprite != null) 
        {
            uiImage.sprite = newSprite; 
        }
    }
}
