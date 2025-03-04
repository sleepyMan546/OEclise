using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = true;

  
    public GameObject chainPrefab;
    public Transform firePoint;

    private Rigidbody2D playerRb;

    public void LookAtPlayer()
    {
       
        if (transform.position.x > player.position.x && !isFlipped) 
        {
           
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true; 
        }
        else if (transform.position.x < player.position.x && isFlipped) 
        {
            
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false; 
        }
    }
  

}
