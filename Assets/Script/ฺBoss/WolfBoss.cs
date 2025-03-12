using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBoss : MonoBehaviour
{
    [SerializeField] private GameObject clawWavePrefab;
    [SerializeField] private GameObject bloodShotPrefab;
    [SerializeField] private GameObject warningPrefab;
    [SerializeField] private GameObject slamAreaPrefab;
    [SerializeField] private float attackCooldown = 5f;
    [SerializeField] private GameObject player;
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] public float attackRange = 3f;
    [SerializeField] private Transform firePoint;

    [Header("Blood Shot Attack")]
    [SerializeField] private float bloodShotSpeed = 10f;       // ความเร็วของกระสุน
    [SerializeField] private int numberOfSeries = 3;            // จำนวนชุดกระสุน
    [SerializeField] private int shotsPerSeries = 5;           // จำนวนกระสุนในแต่ละชุด
    [SerializeField] private float delayBetweenSeries = 0.5f;    // ระยะห่างระหว่างชุด
    [SerializeField] private float shotSpacing = 0.5f;          // ระยะห่างระหว่างกระสุนในชุด

    private Animator anim;
    private EnemyHp hp;
    private float nextAttackTime;
    private Vector3 originalPosition;
    private bool isFacingRight = true;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        hp = GetComponent<EnemyHp>();
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
        nextAttackTime = Time.time;
    }

    void Update()
    {
        FlipTowardsPlayer();

        float distanceToPlayer = Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(player.transform.position.x, 0));
        if (distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
            if (Time.time >= nextAttackTime)
            {
                ChooseAttack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void FlipTowardsPlayer()
    {
        bool playerIsRight = player.transform.position.x > transform.position.x;
        if (playerIsRight && !isFacingRight)
        {
            Flip();
        }
        else if (!playerIsRight && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void MoveTowardsPlayer()
    {
        float targetX = player.transform.position.x;
        float newX = Mathf.MoveTowards(transform.position.x, targetX, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(newX, originalPosition.y, transform.position.z);
    }

    void ChooseAttack()
    {
        int attackType = Random.Range(1, 4);
        switch (attackType)
        {
            case 1:
                StartCoroutine(ClawWaveAttack());
                break;
            case 2:
                StartCoroutine(BloodShotAttack());
                break;
            case 3:
                StartCoroutine(JumpSlamAttack());
                break;
        }
    }

    IEnumerator ClawWaveAttack()
    {
        anim.SetTrigger("Claw");
        yield return new WaitForSeconds(0.5f);

        Quaternion rotation = isFacingRight ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
        GameObject clawWave = Instantiate(clawWavePrefab, transform.position, rotation);
        clawWave.GetComponent<Rigidbody2D>().velocity = clawWave.transform.right * 10f;
    }

    IEnumerator BloodShotAttack()
    {
        anim.SetTrigger("Charge");
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("BloodShot");

        for (int series = 0; series < numberOfSeries; series++)
        {
            for (int i = 0; i < shotsPerSeries; i++)
            {
                Vector3 shotOffset = new Vector3(i * shotSpacing * (isFacingRight ? 1 : -1), 0, 0);
                Vector3 shotPosition = firePoint.position + shotOffset;
                GameObject bloodShot = Instantiate(bloodShotPrefab, shotPosition, Quaternion.identity);

                Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;
                bloodShot.GetComponent<Rigidbody2D>().velocity = direction * bloodShotSpeed; 

                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(delayBetweenSeries);
        }
    }

    IEnumerator JumpSlamAttack()
    {
        anim.SetTrigger("Jump");
        transform.position = new Vector3(transform.position.x, transform.position.y + 50f, transform.position.z);
        Vector3 targetPos = player.transform.position;
        GameObject warning = Instantiate(warningPrefab, targetPos, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        Destroy(warning);
        transform.position = new Vector3(targetPos.x, originalPosition.y, targetPos.z);
        anim.SetTrigger("Slam");
        GameObject slamArea = Instantiate(slamAreaPrefab, targetPos, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(slamArea);
    }
}
