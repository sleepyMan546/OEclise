using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookMove : MonoBehaviour
{
    public float speed = 5f;
    public float pullrange = 2;
    public int damage = 20;

    private Transform target; 

    void Start()
    {
      
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has the tag 'Player'.");
            Destroy(gameObject); 
        }
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = target.position - transform.position;
        direction.Normalize();

        Vector2 velocity = direction * speed;

        transform.Translate(velocity * Time.deltaTime);
        if (Vector2.Distance(transform.position, target.position) < pullrange)
        {
            transform.Translate(velocity * Time.deltaTime);
            
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          

            target = null; 
        }

    }
}
