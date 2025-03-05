using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveAB : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 2f;
    public float arriveDistance = 0.1f;
    private Transform player;

    private Rigidbody2D rb;
    private bool movingToA = true;

    private bool faceleft = true;
    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector2 targetPosition;
        anim.SetBool("Run", true);
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
        if (movingToA)
        {
            targetPosition = pointA.position;
            anim.SetBool("Run", true);

        }
        else
        {
            targetPosition = pointB.position;
            
        }

        if (direction.x > 0 && faceleft)
        {
            Flip();
        }
        else if (direction.x < 0 && !faceleft)
        {
            Flip();
        }
        Debug.Log("Direction" + direction);

        rb.MovePosition(Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime));


        float distance = Vector2.Distance(rb.position, targetPosition);
        if (distance < arriveDistance)
        {
            Debug.Log("Distance");
            movingToA = !movingToA;
        }
    }
    void Flip()
    {
        faceleft = !faceleft;
        transform.localRotation = Quaternion.Euler(0f, faceleft ? 0f : 180f, 0f);
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;



        //firePoint.localRotation = Quaternion.Euler(0f, 0f, faceleft ? 0f : 180f);
    }
}
