using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image enemyHp;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        enemyHp.fillAmount = Mathf.Clamp((float)currentHealth / maxHealth, 0, 1);
    }
    public void TakeDamageEnemy(int damage)
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
