using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineShoot : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    [Header("Burst Settings")]
    public int bulletsPerBurst = 3;
    public float bulletInterval = 0.1f;
    public float fireRate = 1f;

    [Header("Audio")]
    [SerializeField] private AudioSource shootingSoundSource;

    private float nextFireTime = 0f;
    private bool isFiring = false;

    void Start()
    {
        if (shootingSoundSource == null)
        {
            Debug.LogWarning("No audio source assigned to MachineGun!");
        }
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("bulletPrefab or firePoint is not assigned! Check Inspector.");
        }
    }

    void OnDisable()
    {
       
        isFiring = false;
        StopAllCoroutines();
        Debug.Log("MachineShoot disabled, resetting isFiring");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fire1 pressed, checking conditions...");
            if (Time.time >= nextFireTime && !isFiring)
            {
                shootingSoundSource.Play();
                StartCoroutine(FireBurst());
                Debug.Log("MachineShoot triggered");
            }
            else
            {
                if (Time.time < nextFireTime)
                    Debug.Log("Waiting for nextFireTime: " + (nextFireTime - Time.time) + " seconds");
                if (isFiring)
                    Debug.Log("Currently firing, wait for burst to finish");
            }
        }
    }

    IEnumerator FireBurst()
    {
        try
        {
            isFiring = true;
            Debug.Log("Starting FireBurst, isFiring set to true");

            for (int i = 0; i < bulletsPerBurst; i++)
            {
                ShootBullet();
                Debug.Log("Shot bullet " + (i + 1) + " of " + bulletsPerBurst);
                yield return new WaitForSeconds(bulletInterval);
            }
        }
        finally
        {
            isFiring = false;
            nextFireTime = Time.time + fireRate;
            Debug.Log("FireBurst completed, isFiring set to false");
        }
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

            //if (shootingSoundSource != null)
            //{
            //    shootingSoundSource.Play();
            //}
        }
        else
        {
            Debug.LogError("bulletPrefab or firePoint is not assigned during shooting!");
        }
    }
}
