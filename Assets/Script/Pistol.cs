using UnityEngine;

public class Pistol : MonoBehaviour
{
    
    public SpriteRenderer armRenderer;
    public Transform shootPoint; 
    public PlayerMovement playerMovement;

    [Header("Dash Settings")]
    public float dashSpeed = 5f;      
    public float dashDuration = 0.2f; 

    private Rigidbody2D rb;

    private void Start()
    {
        
        if (transform.parent != null)
        {
            playerMovement = transform.parent.GetComponent<PlayerMovement>();
            rb = transform.parent.GetComponent<Rigidbody2D>();
        }
        

        if (shootPoint == null)
        {
            shootPoint = transform; 
        }
    }

    void Update()
    {
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (mousePosition.x < transform.position.x)
        {
            armRenderer.flipY = true;
            if (playerMovement != null)
                playerMovement.facingRight = false;
        }
        else
        {
            armRenderer.flipY = false;
            if (playerMovement != null)
                playerMovement.facingRight = true;
        }

       
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Pistol Dash activated");
            Dash();
        }
    }

    void Dash()
    {
        if (rb == null)
        {
            Debug.LogWarning("ไม่พบ Rigidbody2D ใน Parent");
            return;
        }


        Vector2 dashDirection = shootPoint.right.normalized;
        //float dashDirection = faceRight ? 1f : -1f; 
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        //rb.AddForce(new Vector2(dashDirection * dashSpeed, 0f), ForceMode2D.Impulse);
        rb.AddForce(new Vector2(dashDirection.x * dashSpeed, 0f), ForceMode2D.Impulse);
    }
}
