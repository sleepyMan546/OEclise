using UnityEngine;

public class Ghost : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;  
    private Color color;                    
    public float fadeSpeed = 5f;            

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color; 
    }

    void Update()
    {
       
        color.a -= fadeSpeed * Time.deltaTime;
        spriteRenderer.color = color;

if (color.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    
    public void SetSprite(Sprite sprite, Color startColor)
    {
        spriteRenderer.sprite = sprite;  
        spriteRenderer.color = startColor;  
    }
}
