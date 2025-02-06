using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; 
    public float lifetime = 0.5f; 
    public int damage = 10;

    void Start()
    {
        Destroy(gameObject, lifetime); 
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime); 
    }

    void  OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") 
        {
            EnemyHp targetHealth = collision.GetComponent<EnemyHp>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamageEnemy(damage);
            }

            Destroy(gameObject); 
        }
    }
}
