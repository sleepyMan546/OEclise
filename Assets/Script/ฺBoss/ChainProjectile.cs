using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float pullForce = 20f;
    public float pullDuration = 0.5f;
    private Rigidbody2D rb;
    private Transform targetPlayer;
    private bool pulling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed; 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !pulling)
        {
            pulling = true;
            targetPlayer = collision.transform;
            StartCoroutine(PullPlayer());
        }
    }

    IEnumerator PullPlayer()
    {
        float timer = 0;
        Rigidbody2D playerRb = targetPlayer.GetComponent<Rigidbody2D>();

        while (timer < pullDuration && targetPlayer != null)
        {
            Vector2 pullDirection = (transform.position - targetPlayer.position).normalized;
            playerRb.velocity = pullDirection * pullForce; 
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject); 
    }
}
