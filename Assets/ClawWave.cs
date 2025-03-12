using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawWave : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifetime = 2f;

    void Start()
    {
       
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Hp>().TakeDamage((int)damage);
            Destroy(gameObject);
        }
    }
}
