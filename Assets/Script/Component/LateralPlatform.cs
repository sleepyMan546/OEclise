using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateralPlatform : MonoBehaviour
{
    public float speed = 5f;
    public float totalDistance = 10f; 
    public float startY;

    private Rigidbody2D rb;
    private float distanceTraveled = 0f;
    private Vector2 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position; 

    }

    void FixedUpdate()
    {
        
        Vector2 newPosition = rb.position + Vector2.up * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

       
        distanceTraveled = Vector2.Distance(startPosition, rb.position);

        
        if (distanceTraveled >= totalDistance)
        {
           
            rb.MovePosition(startPosition);
            distanceTraveled = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
          
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 endPoint = startPosition + Vector2.up * totalDistance; 
        Gizmos.DrawLine(startPosition, endPoint); 
    }
}
