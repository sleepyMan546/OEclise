using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlaform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 1.0f;
    public float checkDistance = 0.01f;

    private Vector3 targetPosition;
    private bool movingToA = true;
    private Rigidbody2D rb;
   
    void Start()
    {
        targetPosition = pointB.position;
        rb = GetComponent<Rigidbody2D>();

      
        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogError("MovingPlatform is missing a Collider2D!");
            enabled = false;
        }

    }

    void FixedUpdate()
    {
        if (movingToA)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pointA.position) < checkDistance)
            {
                movingToA = false;
                targetPosition = pointB.position;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pointB.position) < checkDistance)
            {
                movingToA = true;
                targetPosition = pointA.position;
            }
        }
    }

   
   
}
