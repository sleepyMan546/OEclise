using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // �Ӥѭ: ��ͧ�� namespace ���������ҹ UI Image

public class SpriteSwitcher : MonoBehaviour
{
    public Sprite spriteW; 
    public Sprite spriteQ; 

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
    }

   
    void ChangeSprite(Sprite newSprite)
    {
        if (uiImage != null && newSprite != null) 
        {
            uiImage.sprite = newSprite; 
        }
    }
}
