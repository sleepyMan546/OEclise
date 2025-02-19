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
    public WeaponSwitchDop weaponSwitchDop;
    private int airJumpCount = 0; 
    public int airJumpsAllowed = 1; 

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                Jump();
                airJumpCount = 0; 
                Debug.Log("airJump = " + airJumpCount);
            }
            else if (CanAirJump()) 
            {
                AirJump();
            }
        }
        //set animation
        anim.SetBool("Walk", move != 0);

        if (Input.GetKey(KeyCode.Mouse1) && CanDash()) 
        {
            Debug.Log("Dash");
            Dash();
        }
        if (Input.GetKey(KeyCode.E) )
        {
            Debug.Log("Can Dash" + CanDash());
        }

    }
    bool CanDash()  
    {
       
        return weaponSwitchDop != null && weaponSwitchDop.GetCurrentWeapon() == "pistol";
    }
    bool CanAirJump()
    {
        return airJumpCount < airJumpsAllowed && weaponSwitchDop != null && weaponSwitchDop.GetCurrentWeapon() == "shotgun";
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

    void AirJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); 
        anim.SetTrigger("Jump");  
        airJumpCount++; 
    }
}
    