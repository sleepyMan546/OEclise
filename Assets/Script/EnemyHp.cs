using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHp : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image enemyHp;
    private Renderer objRenderer;
    private Color originalColor;
    private Animator anim;
    private Rigidbody2D rb;

    public ParticleSystem deathEffect;

    void Start()
    {
        currentHealth = maxHealth;
        objRenderer = GetComponent<Renderer>();
        originalColor = objRenderer.material.color;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        enemyHp.fillAmount = Mathf.Clamp((float)currentHealth / maxHealth, 0, 1);
    }
    public void TakeDamageEnemy(int damage )
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " Takedamage " + damage + " CurrentHp " + currentHealth);
        anim.SetTrigger("Damage");
        ChangeRed();
        
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void ChangeRed()
    {
        StartCoroutine(ChangeColorRoutine());
    }
  
    private IEnumerator ChangeColorRoutine()
    {
        objRenderer.material.color = Color.red;
        yield return new WaitForSeconds(2f);
        objRenderer.material.color = originalColor;
    }
    void Die()
    {
        Debug.Log(gameObject.name + " ???????!");

        
        if (deathEffect != null)
        {
            
            deathEffect.transform.position = transform.position;
            deathEffect.Play(); 
        }

        Destroy(gameObject); 
    }
}