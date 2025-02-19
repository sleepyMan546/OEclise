using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image healthBar;
    private Renderer objRenderer;
    private Color originalColor;

    void Start()
    {
        currentHealth = maxHealth;
        objRenderer = GetComponent<Renderer>(); 
        originalColor = objRenderer.material.color; 
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp((float)currentHealth / maxHealth, 0, 1);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " Takedamage " + damage + " CurrentHp " + currentHealth);
        ChangeToRed ();

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
    public void ChangeToRed()
    {
        StartCoroutine(ChangeColorRoutine());
    }
    private IEnumerator ChangeColorRoutine()
    {
        objRenderer.material.color = Color.red;
        yield return new WaitForSeconds(1f);
        objRenderer.material.color = originalColor;
    }
}
