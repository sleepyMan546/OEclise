using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 3f;
    public float attackDelay = 0.5f; // ลดเวลาลงมา

    public GameObject chainPrefab;
    public Transform firePoint;

    public GameObject attackIndicatorPrefab; // Prefab วงกลม

    private Transform player;
    private Rigidbody2D rb;
    private Boss boss;

    private bool isAttacking = false;
    private float attackTimer = 0f;
    private GameObject attackIndicatorInstance;
    //private Vector2 indicatorPosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        firePoint = GameObject.FindGameObjectWithTag("FirePoint")?.transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        float distance = Vector2.Distance(player.position, rb.position);

        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (distance <= attackRange && !isAttacking) // ตรวจสอบ isAttacking ด้วย
        {
            // เริ่มการเตรียมโจมตี
            isAttacking = true;
            attackTimer = 0f;
            //ค านวณต าแหน่ง
            Vector3 indicatorPosition = CalculateIndicatorPosition();

            // สร้างวงกลมที่พื้น
            if (attackIndicatorPrefab != null)
            {
                attackIndicatorInstance = Instantiate(attackIndicatorPrefab, indicatorPosition, Quaternion.identity);
                // ปรับขนาด
                if (attackIndicatorInstance.GetComponent<Transform>() != null)
                {
                    attackIndicatorInstance.GetComponent<Transform>().localScale = new Vector3(3, 3, 3); // Adjust to 3x3 for visibility
                }
            }
        }

        if (isAttacking)
        {
            // นับเวลาถอยหลัง
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDelay)
            {
                // ถึงเวลาโจมตี
                animator.SetTrigger("Shoot");
                isAttacking = false; // รีเซ็ตสถานะการโจมตี

                // ทำลายวงกลม
                if (attackIndicatorInstance != null)
                {
                    Destroy(attackIndicatorInstance);
                }
            }
        }
        else
        {
            animator.ResetTrigger("Shoot");
        }
    }

    // Helper Function ในการค านวณต าแหน่งIndicator
    Vector3 CalculateIndicatorPosition()
    {
        Vector3 targetPosition = player.position;

        // ตรวจสอบ line of sight เเบบง่าย
        Vector2 directionToPlayer = (targetPosition - (Vector3)rb.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(rb.position, directionToPlayer, attackRange, LayerMask.GetMask("Ground"));

        // ถือว่า player โดนโจมตีในแนวราบ
        return player.transform.position;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //ท าลายวงกลมแม้เปลี่ยน State
        if (attackIndicatorInstance != null)
        {
            Destroy(attackIndicatorInstance);
        }
    }
}
