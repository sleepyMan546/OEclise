using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 3f; 
    private Transform player; 
    private Rigidbody2D rb;

    private bool faceleft = true;
    public Transform firePoint;
    public Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized; 
            rb.velocity = direction * moveSpeed; 
           
            if (direction.x > 0 && faceleft) 
            {
                Flip();
            }
            else if (direction.x < 0 && !faceleft) 
            {
                Flip();
            }
            anim.SetBool("Run", true);
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