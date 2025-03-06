using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHoming : MonoBehaviour
{
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public float fireRate = 2f;
    public float detectionRange = 15f;

    private Transform player;
    private float nextFireTime;

    [SerializeField] private AudioSource homingShootingSoundSource; // เพิ่มตัวแปร AudioSource สำหรับเสียงยิง Homing

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ตรวจสอบว่าได้กำหนด AudioSource สำหรับเสียงยิง Homing ใน Inspector หรือยัง
        if (homingShootingSoundSource == null)
        {
            Debug.LogError("ไม่ได้กำหนด AudioSource สำหรับเสียงยิง Homing ใน Inspector! กรุณาลาก GameObject ที่มี AudioSource มาใส่ในช่อง Homing Shooting Sound Source ใน Inspector ของสคริปต์ EnemyHoming");
        }
    }


    public void HomingShoot(Transform target, GameObject homingBulletPrefab)
    {
        Vector2 directionToPlayer = (target.position - transform.position).normalized;

        GameObject bullet = Instantiate(homingBulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.velocity = directionToPlayer * bulletSpeed;
        Debug.Log("Shoot");

        // เล่นเสียงยิง Homing (ถ้า AudioSource ถูกกำหนดไว้)
        if (homingShootingSoundSource != null)
        {
            homingShootingSoundSource.Play();
        }
    }
}
