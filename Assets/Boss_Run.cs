using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    public float speed = 2.5f;
    Transform player;
    Rigidbody2D rb;
    Boss boss;
    public float attackRange = 3f;
    public float superspeed = 3f;

    public GameObject chainPrefab; 
    public Transform firePoint;    
    public float shootCooldown = 3f;
    private float lastShootTime = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        if (rb == null) 
        {
            Debug.LogError("Rigidbody not found on " + animator.gameObject.name);
            
            

          
            return; 
        }
        boss = animator.GetComponent<Boss>();
       
        firePoint = GameObject.FindGameObjectWithTag("FirePoint")?.transform;

        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        float distance = Vector2.Distance(player.position, rb.position);

        float currentSpeed = speed; 

        if (distance >= 10f)
        {
            Debug.Log("distance more 10");
           
        }

        Vector2 newPos = Vector2.MoveTowards(rb.position, target, currentSpeed * Time.fixedDeltaTime); 
        rb.MovePosition(newPos);

        if (distance <= attackRange)
        {
            animator.SetTrigger("Shoot");
        }
    }
   
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Shoot");
       
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
