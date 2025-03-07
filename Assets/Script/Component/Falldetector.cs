using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falldetector : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.CompareTag("Player"))
        {
           
            Hp playerHp = collision.gameObject.GetComponent<Hp>();

            
            if (playerHp != null)
            {
                playerHp.Die();
            }
            else
            {
                Debug.LogError("Player is missing Hp component!");
            }
        }
    }
}
