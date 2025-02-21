using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour 
{
    public PlayerMovement playerMovement;
    public SpriteRenderer armRenderer;

    private void Start()
    {
        if (transform.parent != null && transform.parent.name == "Player")
        {
            playerMovement = transform.parent.GetComponent<PlayerMovement>();
        }
        else
        {
            Debug.LogWarning("Pistol is not child of Player");
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
            playerMovement.facingRight = false;


        }
        else
        {
            armRenderer.flipY = false;
            playerMovement.facingRight = true;


        }
    }
}
