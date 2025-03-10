using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terret : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float health = 20f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private int bulletsPerBurst = 5;
    [SerializeField] private float bulletInterval = 0.1f;
    [SerializeField] private float spriteAngleOffset = 90f;
    [SerializeField] private float minAngle = -75f;
    [SerializeField] private float maxAngle = 75f;
    [SerializeField] private float warningTime = 0.5f; 
    [SerializeField] private Color warningColor = Color.red; 

    private float fireTimer = 0f;
    public bool isActive = false;
    public bool isFiring = false;
    private GameObject player;
    private LineRenderer warningLine; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found! Please assign 'Player' tag to the player GameObject.");
        }

       
        warningLine = gameObject.AddComponent<LineRenderer>();
        warningLine.positionCount = 2;
        warningLine.startWidth = 0.05f;
        warningLine.endWidth = 0.05f;
        warningLine.material = new Material(Shader.Find("Sprites/Default"));
        warningLine.startColor = warningColor;
        warningLine.endColor = warningColor;
        warningLine.enabled = false;
    }

    void Update()
    {
        if (!isActive || player == null) return;

        RotateTowardsPlayer();
        UpdateWarningLine(); 

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
            angle -= spriteAngleOffset;
            angle = Mathf.Clamp(angle, minAngle, maxAngle);
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void UpdateWarningLine()
    {
        if (warningLine != null && player != null)
        {
            warningLine.SetPosition(0, firePoint.position);
            warningLine.SetPosition(1, player.transform.position);
        }
    }

    IEnumerator FireBurst()
    {
        isFiring = true;

       
        warningLine.enabled = true;
        yield return new WaitForSeconds(warningTime);
        warningLine.enabled = false;

      
        for (int i = 0; i < bulletsPerBurst; i++)
        {
            Fire();
            yield return new WaitForSeconds(bulletInterval);
        }
        isFiring = false;
    }

    void Fire()
    {
        if (player != null)
        {
            Instantiate(projectilePrefab, firePoint.position, transform.rotation);
            Debug.Log("Turret fired a bullet!");
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

    void OnDestroy()
    {
        if (warningLine != null)
        {
            Destroy(warningLine.material);
        }
    }

}
