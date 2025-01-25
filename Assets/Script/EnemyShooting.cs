using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyShooting : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public Transform firePoint; 
    public float bulletSpeed = 5f; 
    public float fireRate = 1f; 
    private float nextFireTime = 0f; 
    private Transform player;

 

    void Update()
    {
       
        if (Time.time >= nextFireTime) 
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // ตั้งค่าเวลาสำหรับการยิงครั้งต่อไป
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;
    }
}
