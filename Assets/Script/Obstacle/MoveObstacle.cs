using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    public Transform player; 
    public float followSpeed = 5f;
  
 

    private Vector3 lastPlayerPosition;
    private bool isPlayerDashing = false;
    

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        lastPlayerPosition = player.position;
    }

    void Update()
    {
        MoveTowardsPlayer();
        
        
    }

    void MoveTowardsPlayer()
    {
        if (!isPlayerDashing)
        {
        
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

   

  
    
}