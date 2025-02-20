using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlaform : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool movingToA = true;
    public Transform pointA;
    public Transform pointB;
    public float speed = 1.0f;
    private Rigidbody2D rb;

    void Start()
    {
        targetPosition = pointB.position; 
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (movingToA)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pointA.position) < 0.01f) 
            {
                movingToA = false;
                targetPosition = pointB.position;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pointB.position) < 0.01f) 
            {
                movingToA = true;
                targetPosition = pointA.position;
            }
        }
    }
}
