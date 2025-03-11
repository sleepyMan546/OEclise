using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineShoot : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab; // Prefab ของกระสุน
    public Transform firePoint;     // จุดยิงกระสุน
    public float bulletSpeed = 10f; // ความเร็วของกระสุน

    [Header("Burst Settings")]
    public int bulletsPerBurst = 3;  // จำนวนกระสุนต่อชุด
    public float bulletInterval = 0.1f; // ระยะห่างระหว่างนัดในชุด (วินาที)
    public float fireRate = 1f;      // ความถี่ในการยิงชุด (วินาที)

    [Header("Audio")]
    [SerializeField] private AudioSource shootingSoundSource; // เสียงยิง

    private float nextFireTime = 0f; // เวลาที่จะยิงชุดถัดไป
    private bool isFiring = false;   // ตรวจสอบว่ากำลังยิงอยู่หรือไม่

    void Start()
    {
        // ตรวจสอบว่า AudioSource ถูกกำหนดหรือไม่
        if (shootingSoundSource == null)
        {
            Debug.LogError("No audio source assigned to MachineGun!");
        }
    }

    void Update()
    {
      
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime && !isFiring)
        {
            StartCoroutine(FireBurst());
        }
    }

    
    IEnumerator FireBurst()
    {
        isFiring = true;
        for (int i = 0; i < bulletsPerBurst; i++)
        {
            ShootBullet();
            yield return new WaitForSeconds(bulletInterval); 
        }
        isFiring = false;
        nextFireTime = Time.time + fireRate; 
    }

    
    void ShootBullet()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = firePoint.right * bulletSpeed;
            }
            else
            {
                Debug.LogWarning("No Rigidbody2D found on bulletPrefab!");
            }

           
            if (shootingSoundSource != null)
            {
                shootingSoundSource.Play();
            }
        }
        else
        {
            Debug.LogError("bulletPrefab or firePoint is not assigned!");
        }
    }
}
