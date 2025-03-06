using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public float fireRate = 1f;
    public float detectionRange = 10f;

    private Transform player;
    private float nextFireTime = 0f;
    public Animator anim;

    [SerializeField] private AudioSource enemyShootingSoundSource; // เพิ่มตัวแปร AudioSource สำหรับเสียงยิงศัตรู

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

        // ตรวจสอบว่าได้กำหนด AudioSource สำหรับเสียงยิงศัตรูใน Inspector หรือยัง
        if (enemyShootingSoundSource == null)
        {
            Debug.LogError("ไม่ได้กำหนด AudioSource สำหรับเสียงยิงศัตรูใน Inspector! กรุณาลาก GameObject ที่มี AudioSource มาใส่ในช่อง Enemy Shooting Sound Source ใน Inspector ของสคริปต์ EnemyShooting");
        }
    }

    void Update()
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
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = (player.position - firePoint.position).normalized;
        rb.velocity = direction * bulletSpeed;
        anim.SetTrigger("Shoot");

        // เล่นเสียงยิงศัตรู (ถ้า AudioSource ถูกกำหนดไว้)
        if (enemyShootingSoundSource != null)
        {
            enemyShootingSoundSource.Play();
        }
    }
}