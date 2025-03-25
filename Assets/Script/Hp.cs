using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Transactions;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.TextCore.Text;
using UnityEngine.Events;


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
    public TMPro.TextMeshProUGUI hpText;
    public Animator animator;
    public UnityEvent onTakeDamage;
    private bool isGodMode = false;

    //Barier
    [SerializeField] private GameObject barrierPrefab; 
    [SerializeField] private float barrierDuration = 5f; 
    [SerializeField] private float barrierCooldown = 10f; 
    private GameObject currentBarrier; 
    private bool isBarrierActive = false; 
    private float currentBarrierCooldown = 0f; 
   

    void Start()
    {
        currentHealth = maxHealth;
        objRenderer = GetComponent<Renderer>();
        originalColor = objRenderer.material.color;
        anim = GetComponent<Animator>();
        UpdateHealthBar();
        animator = GetComponent<Animator>();
      
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp((float)currentHealth / maxHealth, 0, 1);
        UpdateHealthUI();

        if (Input.GetKeyDown(KeyCode.G))
        {
            isGodMode = !isGodMode;
            Debug.Log("God Mode: " + (isGodMode ? "ON" : "OFF"));
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SceneManager.LoadScene("SceneNameHere");
        }

        // ลด Cooldown ของบาเรีย
        if (currentBarrierCooldown > 0)
        {
            currentBarrierCooldown -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!this.isGodMode && !isBarrierActive) 
        {
            currentHealth -= damage;
            Debug.Log(gameObject.name + " Takedamage " + damage + " CurrentHp " + currentHealth);
            anim.SetTrigger("Take");
            ChangeToRed();
            UpdateHealthUI();
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            onTakeDamage.Invoke();


            if (currentHealth <= 0)
            {
                Die();
            }
        }
        else if (isGodMode)
        {
            Debug.Log(gameObject.name + " is in God Mode, no damage taken!");
        }
        else if (isBarrierActive)
        {
            Debug.Log(gameObject.name + " is protected by Barrier, no damage taken!");
        }
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " Die");
        gameObject.SetActive(false);
        TrasitionScene.Instance.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeToRed()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeColorRoutine());
    }

    private IEnumerator ChangeColorRoutine()
    {
        Physics2D.IgnoreLayerCollision(7, 9, true);
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < numberOfFlashes; i++)
        {
            foreach (Renderer rend in renderers)
            {
                rend.material.color = new Color(1, 0, 0, 0.5f);
            }
            yield return new WaitForSeconds(iFrameDelay / (numberOfFlashes * 2));

            foreach (Renderer rend in renderers)
            {
                rend.material.color = originalColor;
            }
            yield return new WaitForSeconds(iFrameDelay / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 9, false);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isGodMode = false;
        StopAllCoroutines();
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderers)
        {
            rend.material.color = originalColor;
        }
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        Debug.Log("UpdateHealthBar called! Current Health: " + currentHealth);
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
     
    }
    void UpdateHealthUI()
    {
        if (hpText != null)
        {
            hpText.text = currentHealth.ToString();
            hpText.color = currentHealth < 170 ? Color.red : Color.white; 
           
        }
    }


    public bool ActivateBarrier()
    {
        if (currentBarrierCooldown > 0 || isBarrierActive)
        {
            Debug.Log("Barrier is on cooldown or already active! Cooldown remaining: " + currentBarrierCooldown);
            return false;
        }

        StartCoroutine(BarrierCoroutine());
        return true;
    }

    private IEnumerator BarrierCoroutine()
    {
        isBarrierActive = true;
        currentBarrier = Instantiate(barrierPrefab, transform.position, Quaternion.identity);
        currentBarrier.transform.SetParent(transform); 
        currentBarrier.tag = "Barrier"; 

        yield return new WaitForSeconds(barrierDuration);

        DeactivateBarrier();
    }

    private void DeactivateBarrier()
    {
        if (currentBarrier != null)
        {
            Destroy(currentBarrier);
        }
        isBarrierActive = false;
        currentBarrierCooldown = barrierCooldown;
        Debug.Log("Barrier deactivated. Cooldown started: " + currentBarrierCooldown);
    }

  
    public bool IsBarrierActive()
    {
        return isBarrierActive;
    }

    public float GetBarrierCooldown()
    {
        return currentBarrierCooldown;
    }

}
