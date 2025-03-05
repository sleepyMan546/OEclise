using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_State : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;
    public float arriveDistance = 0.1f;

    private Rigidbody2D rb;
    private bool movingToA = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 targetPosition;

      
        if (movingToA)
        {
            targetPosition = pointA.position;
        }
        else
        {
            targetPosition = pointB.position;
        }

      
        rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime));

       
        float distance = Vector2.Distance(rb.position, targetPosition);
        if (distance < arriveDistance)
        {
            
            movingToA = !movingToA;
        }
    }
}













    // Update is called once per frame
  


