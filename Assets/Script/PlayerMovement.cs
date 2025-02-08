using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator anim;
   
    public float jumpForce = 10f;
    private bool isGrounded;
    public bool facingRight = true;  
    public SpriteRenderer bodyRenderer; 
    private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask grounlayer;
    [SerializeField] private LayerMask walllayer;
    private bool faceRight = true;
    public float dashSpeed = 1f;
    public float dashDuration = 0.2f;
    public bool isDashing = false;
    private float direction  ;
    public float moveDirection;
    public Transform shootPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    
    void Update()
    {
      float move = Input.GetAxis("Horizontal");
      rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

      
        if (facingRight)
        {
            bodyRenderer.flipX = false;
        }
        else
        {
            bodyRenderer.flipX = true;
        }
        
        if (Input.GetKey(KeyCode.Space) && IsGrounded()) {
            Jump();
        
        }
        //set animation
        anim.SetBool("Walk", move != 0);

        if(Input.GetKey(KeyCode.LeftShift) && HasPistrol())
        {
            Debug.Log("Dash");
            Dash();
        }
       
       
    }
    bool HasPistrol()
    {
        return GetComponent<Hold_Pistol>() != null;
        
    }
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
       anim.SetTrigger("Jump");
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        return raycastHit.collider != null;
    }


  void Dash()
    {
        Vector2 dashDirection = shootPoint.right.normalized;
        //float dashDirection = faceRight ? 1f : -1f; 
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        //rb.AddForce(new Vector2(dashDirection * dashSpeed, 0f), ForceMode2D.Impulse);
        rb.AddForce(new Vector2(dashDirection.x * dashSpeed, 0f), ForceMode2D.Impulse);
    }
 public void Flip()
    {
        faceRight = !faceRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
    