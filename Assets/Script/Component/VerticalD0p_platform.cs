using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalD0p_platform : MonoBehaviour
{
    public float Speed = 2f;
    private Rigidbody2D rb;
    private Transform playerTransform = null; 
    private bool isPlayerOnPlatform = false;
    private bool shouldMoveUp = false;
    private float initialY; 

    public float floatHeight = 1f;
    public float gravityScale = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; 
        rb.gravityScale = 0; 
        initialY = transform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && !isPlayerOnPlatform)
        {
            playerTransform = collision.transform;
            isPlayerOnPlatform = true;
            shouldMoveUp = true;
            rb.velocity = Vector2.up * Speed; 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && isPlayerOnPlatform)
        {
            playerTransform = null;
            isPlayerOnPlatform = false;
            shouldMoveUp = false;
            rb.velocity = Vector2.down * Speed * gravityScale; 
        }
    }

    void FixedUpdate()
    {
        if (rb.bodyType == RigidbodyType2D.Kinematic)
        {
            if (shouldMoveUp && isPlayerOnPlatform)
            {
                Vector2 targetPosition = new Vector2(transform.position.x, initialY + floatHeight);
                rb.MovePosition(Vector2.Lerp(rb.position, targetPosition, Speed * Time.fixedDeltaTime));
            }
            else if (!isPlayerOnPlatform && rb.velocity.y < 0)
            {
                Vector2 targetPosition = new Vector2(transform.position.x, initialY);
                rb.MovePosition(Vector2.Lerp(rb.position, targetPosition, Speed * gravityScale * Time.fixedDeltaTime));
            }
            else if (!isPlayerOnPlatform && rb.velocity.y == 0)
            {
                rb.MovePosition(Vector2.Lerp(rb.position, new Vector2(transform.position.x, initialY), 0.5f * Time.fixedDeltaTime));
            }
        }
  
    }

    void Update()
    {
        if (isPlayerOnPlatform && playerTransform != null)
        {
          
        }
    }
}
