using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodShoot : MonoBehaviour
{
    [SerializeField] private float damage = 15f;
    [SerializeField] private float lifetime = 3f;

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
