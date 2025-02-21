using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgundop : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    public int bulletCount = 10;
    public float spreadAngle = 20f;

    public ParticleSystem muzzleFlashEffect;
    public GameObject muzzleFlashPosition;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            ShootShotgun();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootShotgun()
    {
        if (muzzleFlashEffect != null && muzzleFlashPosition != null)
        {
         
            muzzleFlashEffect.transform.position = muzzleFlashPosition.transform.position;
            muzzleFlashEffect.transform.rotation = muzzleFlashPosition.transform.rotation; 
            muzzleFlashEffect.Play();
        }

        for (int i = 0; i < bulletCount; i++)
        {
            float spread = Random.Range(-spreadAngle / 2, spreadAngle / 2);
            Quaternion bulletRotation = Quaternion.Euler(0, 0, spread);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * bulletRotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = bullet.transform.right * bulletSpeed;
        }
    }
}
