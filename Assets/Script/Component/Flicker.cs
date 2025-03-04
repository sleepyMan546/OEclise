using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    public float flickerSpeed = 5f; 
    public float minAlpha = 0.2f; 
    public float maxAlpha = 1f; 

    private SpriteRenderer spriteRenderer;
    private float currentAlpha;
    private bool isFadingIn = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Flicker script requires a SpriteRenderer component.");
            enabled = false; 
            return;
        }
        currentAlpha = spriteRenderer.color.a; 
    }

    void Update()
    {
        // ปรับค่า Alpha
        if (isFadingIn)
        {
            currentAlpha += flickerSpeed * Time.deltaTime;
            if (currentAlpha >= maxAlpha)
            {
                currentAlpha = maxAlpha;
                isFadingIn = false;
            }
        }
        else
        {
            currentAlpha -= flickerSpeed * Time.deltaTime;
            if (currentAlpha <= minAlpha)
            {
                currentAlpha = minAlpha;
                isFadingIn = true;
            }
        }

      
        Color color = spriteRenderer.color;
        color.a = currentAlpha;
        spriteRenderer.color = color;
    }
}
