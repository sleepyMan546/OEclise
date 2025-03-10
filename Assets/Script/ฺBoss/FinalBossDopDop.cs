using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossDopDop : MonoBehaviour
{
    [Header("Boss Settings")]
    public float spawnTurretCooldown = 5f; 
    public float enragedSpawnCooldown = 2f; 
    public int enragedLaserShots = 3; 
    private bool isEnraged = false; 

    [Header("References")]
    public GameObject turretPrefab;
    public Transform[] turretSpawnPoints;
    public GameObject laserPrefab;
    public Transform laserSpawnPoint;
    private EnemyHp enemyHp;

    private float nextTurretSpawnTime;

    public GameObject targetPlayer;

    void Start()
    {
        enemyHp = GetComponent<EnemyHp>();
        nextTurretSpawnTime = Time.time + spawnTurretCooldown;
    }

    void Update()
    {
        if (Time.time >= nextTurretSpawnTime)
        {
            SpawnTurret();
            nextTurretSpawnTime = Time.time + (isEnraged ? enragedSpawnCooldown : spawnTurretCooldown);
        }

        if (enemyHp.currentHealth <= enemyHp.maxHealth / 2 && !isEnraged)
        {
            EnterEnragedMode();
        }
    }

    void SpawnTurret()
    {
        Transform spawnPoint = turretSpawnPoints[Random.Range(0, turretSpawnPoints.Length)];
        Instantiate(turretPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("Boss spawned a new turret!");
    }

    void EnterEnragedMode()
    {
        Debug.Log("Boss is ENRAGED! Attacks are now faster!");
        isEnraged = true;

      
        StartCoroutine(FireEnragedLaser());
    }

    IEnumerator FireEnragedLaser()
    {
        for (int i = 0; i < enragedLaserShots; i++)
        {
            ShootLaser();
            yield return new WaitForSeconds(1f);
        }
    }
    void PrepareLaser()
    {
        Debug.Log("Boss is charging laser...");

        
        if (laserSpawnPoint != null)
        {
            GameObject warningLaser = Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
            Renderer laserRenderer = warningLaser.GetComponent<Renderer>();

            if (laserRenderer != null)
            {
                laserRenderer.material.color = Color.red;
            }

            Destroy(warningLaser, 1.5f); 
            StartCoroutine(ShootLaserAfterDelay(1.5f)); 
        }
    }

    IEnumerator ShootLaserAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShootLaser();
    }

    void ShootLaser()
    {
        Debug.Log("Boss fires laser!");

        if (laserSpawnPoint != null)
        {
            Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
        }
    }

    public void TakeDamageBoss(int damage)
    {
        enemyHp.TakeDamageEnemy(damage);
    }
}
