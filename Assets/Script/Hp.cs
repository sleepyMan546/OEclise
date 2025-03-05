using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hp : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image healthBar;
    private Renderer objRenderer;
    private Color originalColor;
    private Animator anim;
    private CheckpointSystem checkpointSystem;
    [SerializeField] private int numberOfFlashes;
    [SerializeField] private float iFrameDelay;


    void Start()
    {
        currentHealth = maxHealth;
        objRenderer = GetComponent<Renderer>(); 
        originalColor = objRenderer.material.color;
        anim = GetComponent<Animator>();
        checkpointSystem = FindObjectOfType<CheckpointSystem>();
        UpdateHealthBar();
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp((float)currentHealth / maxHealth, 0, 1);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " Takedamage " + damage + " CurrentHp " + currentHealth);
        anim.SetTrigger("Take");
        ChangeToRed ();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + "Die");
        //Destroy(gameObject);
        checkpointSystem.RespawnPlayer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ChangeToRed()
    {
        StartCoroutine(ChangeColorRoutine());
    }
    private IEnumerator ChangeColorRoutine()
    {
        //objRenderer.material.color = Color.red;
        //yield return new WaitForSeconds(1f);
        //objRenderer.material.color = originalColor;
        Physics2D.IgnoreLayerCollision(7, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            objRenderer.material.color = new Color(1,0,0,0.5f);
            yield return new WaitForSeconds(iFrameDelay /(numberOfFlashes*2));
            objRenderer.material.color = originalColor;
            yield return new WaitForSeconds(iFrameDelay / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 9, false);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth; 
        UpdateHealthBar();
    }
    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}
