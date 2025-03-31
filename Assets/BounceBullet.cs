using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 0.5f;
    public int damage = 10;
    public int maxBounces = 3; // Maximum number of bounces before the bullet is destroyed
    private int currentBounces = 0; // Track the number of bounces

    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on bullet!");
            return;
        }

        // Set the initial velocity of the bullet
        rb.velocity = transform.right * speed;

        // Destroy the bullet after its lifetime
        Destroy(gameObject, lifetime);

        // Ignore collisions between bullets
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bullet"), LayerMask.NameToLayer("Bullet"));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bullet Hit: " + collision.gameObject.name + " | Layer: " + LayerMask.LayerToName(collision.gameObject.layer));

        // Check if the bullet hits an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHp targetHealth = collision.gameObject.GetComponent<EnemyHp>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamageEnemy(damage);
            }

            Destroy(gameObject); // Destroy the bullet on hitting an enemy
        }
        else
        {
            // Increment the bounce counter
            currentBounces++;

            // Destroy the bullet if it has bounced too many times
            if (currentBounces >= maxBounces)
            {
                Destroy(gameObject);
            }
            else
            {
                // Reflect the bullet's velocity based on the collision normal
                Vector2 normal = collision.contacts[0].normal;
                Vector2 newVelocity = Vector2.Reflect(rb.velocity, normal);
                rb.velocity = newVelocity.normalized * speed; // Maintain the same speed after bouncing
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If you still need trigger-based interactions (e.g., for non-physical objects), you can keep this
        Debug.Log("Bullet Trigger: " + collision.gameObject.name + " | Layer: " + LayerMask.LayerToName(collision.gameObject.layer));
        if (collision.gameObject.CompareTag("Player"))
        {
            // Ignore collisions with the player
            return;
        }
    }
}
