using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    public PlayerMovement playerMovement; 
    public SpriteRenderer armRenderer;


    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);


        if (mousePosition.x < transform.position.x)
        {
            armRenderer.flipY = true;  
            playerMovement.facingRight = false;
            

        }
        else
        {
            armRenderer.flipY = false; 
            playerMovement.facingRight = true;
           

        }
    }
}