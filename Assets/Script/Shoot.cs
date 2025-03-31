using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;
    [SerializeField] private ParticleSystem shootEffect; 
    [SerializeField] private GameObject muzzleFlashPrefab;

    [SerializeField] private AudioSource shootingSoundSource;

    void Start()
    {
        
        if (shootingSoundSource == null)
        {
            Debug.LogError("No audio sourch");
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shooting();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shooting()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;

        
        if (shootingSoundSource != null)
        {
            shootingSoundSource.Play();
        }
        if (shootEffect != null)
        {
            ParticleSystem effect = Instantiate(shootEffect, firePoint.position, firePoint.rotation);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration); 
        }

        
        if (muzzleFlashPrefab != null)
        {
            GameObject muzzleFlash = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);
            Destroy(muzzleFlash, 0.1f); 
        }
    }
}