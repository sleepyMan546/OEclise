using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaserBeam : MonoBehaviour
{
    [SerializeField] private GameObject laserBeamPrefab; // Prefab ของเลเซอร์บีม
    [SerializeField] private Transform firePoint; // จุดยิงเลเซอร์ (ควรอยู่ที่ปากกระบอกปืน)
    [SerializeField] private float fireRate = 2f; // ความถี่การยิง (หน่วงเวลาระหว่างชุด)
    [SerializeField] private float health = 20f; // HP ของป้อมปืน
    [SerializeField] private float rotationSpeed = 5f; // ความเร็วในการหมุน
    [SerializeField] private int beamsPerBurst = 1; // จำนวนเลเซอร์ต่อชุด
    [SerializeField] private float beamInterval = 0.5f; // หน่วงเวลาระหว่างเลเซอร์ในชุด
    [SerializeField] private float spriteAngleOffset = 90f; // ปรับมุมเริ่มต้นของ Sprite
    [SerializeField] private float minAngle = -10f; // มุมขั้นต่ำ
    [SerializeField] private float maxAngle = 100f; // มุมสูงสุด

    private float fireTimer = 0f;
    public bool isActive = false;
    public bool isFiring = false;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found! Please assign 'Player' tag to the player GameObject.");
        }
    }

    void Update()
    {
        if (!isActive || player == null) return;

        RotateTowardsPlayer();

       
        if (!isFiring)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                StartCoroutine(FireBurst());
                fireTimer = 0f;
            }
        }
    }

    
    void RotateTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= spriteAngleOffset; // ปรับทิศทางให้เข้ากับ Sprite
            angle = Mathf.Clamp(angle, minAngle, maxAngle); // จำกัดมุมหมุน (-10 ถึง 100 องศา)
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    IEnumerator FireBurst()
    {
        isFiring = true;
        for (int i = 0; i < beamsPerBurst; i++)
        {
            Fire();
            yield return new WaitForSeconds(beamInterval);
        }
        isFiring = false;
    }

    void Fire()
    {
        if (player != null && laserBeamPrefab != null && firePoint != null)
        {
            
            Instantiate(laserBeamPrefab, firePoint.position, transform.rotation);
            Debug.Log("Turret fired a laser beam!");
        }
        else
        {
            Debug.LogWarning("Missing laserBeamPrefab, firePoint, or player reference!");
        }
    }

    public void Activate()
    {
        isActive = true;
    }

    public bool IsActive()
    {
        return isActive;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            isActive = false;
            gameObject.SetActive(false);
            Debug.Log("Turret Destroyed!");
        }
    }
}
