using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homming_Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float rotateSpeed = 200f; 
    public float lifetime = 3f;
    public int damage = 30;
    public LayerMask whatIsEnemy; 
    public LayerMask playerBulletLayer; 
    public bool Homing;

    private Rigidbody2D rb;
    private Transform target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

       
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        Debug.Log("Find player");

       
        Destroy(gameObject, lifetime);

     
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("BulletBoss"), LayerMask.NameToLayer("BulletBoss"), true);
    }



    void Update()
    {
        if (Homing)
        {
            HomingShot();
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }


    }
    void HomingShot()
    {
        if (target != null)
        {
            Vector2 direction = (Vector2)target.position - rb.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            
            rb.angularVelocity = -rotateAmount * 200;

         
            rb.velocity = transform.up * speed;
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
        else
       if (collision.gameObject.tag != "Enemy")
        {
            Hp targetHealth = collision.GetComponent<Hp>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
               
            }
            Destroy(gameObject);

        }
    }
}
