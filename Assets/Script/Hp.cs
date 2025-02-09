using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    public int maxHealth = 100; 
    public int currentHealth; 
    public Image healthBar;

    void Start()
    {
        currentHealth = maxHealth; 
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp((float)currentHealth / maxHealth, 0, 1);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " Takedamage " + damage + " CurrentHp " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); 
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " ???????!");
        Destroy(gameObject); 
    }
}
