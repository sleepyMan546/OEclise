using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mecha_Shooting : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public List<Transform> firePoints;
    public float bulletSpeed = 5f;
    public float fireRate = 1f;
    public float detectionRange = 10f;
    public float spreadAngle = 15f; 

    private Transform player;
    private float nextFireTime = 0f;
    public Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    public void Normal_Shoot()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        bool canSeePlayer = CheckLineOfSight();

        if (distanceToPlayer <= detectionRange && canSeePlayer && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    bool CheckLineOfSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position, detectionRange, LayerMask.GetMask("Obstacles", "Player"));

        if (hit.collider != null)
        {
            return hit.collider.CompareTag("Player");
        }
        return false;
    }

    public void Shoot()
    {
        anim.SetTrigger("Shoot");

        foreach (Transform firePoint in firePoints)
        {
            float randomSpread = Random.Range(-spreadAngle, spreadAngle); 
            Quaternion spreadRotation = Quaternion.Euler(0, 0, randomSpread);

            GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation * spreadRotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 direction = spreadRotation * (player.position - firePoint.position).normalized; 
            rb.velocity = direction * bulletSpeed;
        }
    }
}
