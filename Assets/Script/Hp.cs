using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    public int maxHealth = 100; 
    public int currentHealth; 

    void Start()
    {
        currentHealth = maxHealth; 
    }

   
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // ?? HP ??????????????
        Debug.Log(gameObject.name + " Takedamage " + damage + " CurrentHp " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); 
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " ???????!");
        Destroy(gameObject); // ????? GameObject
    }
}
