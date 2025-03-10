using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    private Renderer laserRenderer;

    void Start()
    {
        laserRenderer = GetComponent<Renderer>();
        if (laserRenderer != null)
        {
            laserRenderer.material.color = Color.red; 
            Invoke("ChangeToNormalColor", 1.5f); 
        }
    }

    void ChangeToNormalColor()
    {
        if (laserRenderer != null)
        {
            laserRenderer.material.color = Color.white; 
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Hp playerHp = other.GetComponent<Hp>();
            if (playerHp != null)
            {
                playerHp.TakeDamage(damage);
                Debug.Log("Laser hit the player! Dealing " + damage + " damage.");
            }
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
