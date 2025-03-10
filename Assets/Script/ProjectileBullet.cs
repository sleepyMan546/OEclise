using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {

        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Hp playerHp = collision.GetComponent<Hp>();
            if (playerHp != null)
            {
                playerHp.TakeDamage((int)damage);
            }
            Destroy(gameObject);
        }
    }
}
