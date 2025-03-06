using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingObstacle : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public float fireRate = 1f;
    public float detectionRange = 10f;
    public LayerMask targetLayer;

    private Transform player;
    private float nextFireTime = 0f;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    void Update()
    {
        if (player == null) return;
        Debug.DrawLine(transform.position, player.position, Color.red);
        CheckForPlayer(); 
    }

    void CheckForPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange && Time.time >= nextFireTime && CheckLineOfSight())
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    bool CheckLineOfSight()
    {
        Vector2 directionToPlayer = player.position - transform.position;
        int layerMask = LayerMask.GetMask("Player") & ~LayerMask.GetMask("Bullet");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange, LayerMask.GetMask("Player"));
        Debug.DrawRay(transform.position, directionToPlayer.normalized * detectionRange, Color.green);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        else
        {

            return true;

        }

    }

    void Shoot()
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (player.position - firePoint.position).normalized;
        rb.velocity = direction * bulletSpeed;
        
    }
}
