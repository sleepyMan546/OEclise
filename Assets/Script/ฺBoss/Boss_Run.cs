using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Boss_Run : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange = 3f;
    public float attackDelay = 0.5f;

    public GameObject chainPrefab;
    public Transform firePoint;

    public GameObject attackIndicatorPrefab;

    private Transform player;
    private Rigidbody2D rb;
    private Boss boss;

    private bool isAttacking = false;
    private float attackTimer = 0f;
    private GameObject attackIndicatorInstance;
    public int triggerHp = 2000;

    private EnemyHp bossHealth;
    //private Vector2 indicatorPosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        firePoint = GameObject.FindGameObjectWithTag("FirePoint")?.transform;

        bossHealth = animator.GetComponent<EnemyHp>();
        if (bossHealth == null)
        {
            Debug.LogError("BossHealth component missing!");

        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        float distance = Vector2.Distance(player.position, rb.position);

        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (distance <= attackRange && !isAttacking)
        {

            isAttacking = true;
            attackTimer = 0f;

            Vector3 indicatorPosition = CalculateIndicatorPosition();

            if (attackIndicatorPrefab != null)
            {
                attackIndicatorInstance = Instantiate(attackIndicatorPrefab, indicatorPosition, Quaternion.identity);


            }
        }

        if (isAttacking)
        {

            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDelay)
            {

                animator.SetTrigger("Shoot");
                isAttacking = false;


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
        if (bossHealth != null && bossHealth.currentHealth < triggerHp)
        {
            animator.SetTrigger("Pull");
        }
    }


    Vector3 CalculateIndicatorPosition()
    {
        Vector3 targetPosition = player.position;


        Vector2 directionToPlayer = (targetPosition - (Vector3)rb.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(rb.position, directionToPlayer, attackRange, LayerMask.GetMask("Ground"));

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





