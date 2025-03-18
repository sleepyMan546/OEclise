using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalD0p_platform : MonoBehaviour
{
   public Transform child;         
    public Transform parent;        
    public float Speed = 2f;      
    private Rigidbody2D rb;         
    private float targetSpeed;      
    private bool isPlayerOnPlatform = false; 
    private float exitDelay = 0.1f; 
    private float exitTimer = 0f;    
    private bool isChangingDirection = false; 
    private float changeDirectionPause = 0.5f; 
    private float pauseTimer = 0.5f;   

    private void Start()
    {
        parent = transform;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; 
        targetSpeed = Speed; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("platform"))
        {
            isChangingDirection = true; 
            targetSpeed = -Speed; 
            pauseTimer = changeDirectionPause; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            child = collision.transform;
            child.transform.SetParent(parent);
            isPlayerOnPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
            exitTimer = exitDelay; 
        }
    }

    private void FixedUpdate()
    {
       
        if (isChangingDirection)
        {
            pauseTimer -= Time.fixedDeltaTime;
            if (pauseTimer <= 0)
            {
                Speed = targetSpeed; 
                isChangingDirection = false;
            }
            return; 
        }

       
        Vector2 newPosition = rb.position + new Vector2(0, Speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }

    private void Update()
    {
      
        if (!isPlayerOnPlatform && exitTimer > 0)
        {
            exitTimer -= Time.deltaTime;
            if (exitTimer <= 0 && child != null)
            {
                
                RaycastHit2D hit = Physics2D.Raycast(child.position, Vector2.down, 0.3f, LayerMask.GetMask("Platform"));
                if (hit.collider == null || hit.collider.gameObject != gameObject)
                {
                    child.transform.SetParent(null);
                    child = null;
                }
            }
        }
    }
}
