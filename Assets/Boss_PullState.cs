using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Boss_PullState : StateMachineBehaviour
{
    [Header("Homing Settings")]
    public float fireRate = 2f; 
    private float nextFireTime = 0f;
   
    private UnityEngine.Transform player;
    private EnemyHoming homing;

    public GameObject homingBulletPrefab;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("No Player found with tag 'Player'!");
            animator.enabled = false;
            return;
        }

        homing = animator.GetComponent<EnemyHoming>();
        if (homing == null)
        {
            Debug.LogError("EnemyHoming component missing!");
            animator.enabled = false;
            return;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null || homing == null) return;

        if (Time.time > nextFireTime)
        {
            homing.HomingShoot(player, homingBulletPrefab);
            nextFireTime = Time.time + (1f / fireRate);
        }
    }
}
