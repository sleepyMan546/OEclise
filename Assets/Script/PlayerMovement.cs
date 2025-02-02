using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
   
    public float jumpForce = 10f;
    private bool isGrounded;
    public bool facingRight = true;  
    public SpriteRenderer bodyRenderer; 

    private bool faceRight = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
      float move = Input.GetAxis("Horizontal");
      rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

      if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); 
        }
        if (facingRight)
        {
            bodyRenderer.flipX = false;
        }
        else
        {
            bodyRenderer.flipX = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") 
        {
            isGrounded = false;
        }
    }

    void Flip()
    {
        faceRight = !faceRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
    