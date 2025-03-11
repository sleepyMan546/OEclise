using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSkill : MonoBehaviour
{
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private float shieldDuration = 5f;
    [SerializeField] private float shieldHealth = 50f;
    [SerializeField] private float cooldown = 10f;

    private GameObject currentShield;
    private float currentCooldown = 0f;
    private bool isShieldActive = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && currentCooldown <= 0 && !isShieldActive)
        {
            ActivateShield();
        }

        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    void ActivateShield()
    {
        currentShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
        currentShield.transform.SetParent(transform);
        isShieldActive = true;
        StartCoroutine(ShieldDuration());
    }

    IEnumerator ShieldDuration()
    {
        yield return new WaitForSeconds(shieldDuration);
        DestroyShield();
    }

    void DestroyShield()
    {
        if (currentShield != null)
        {
            Destroy(currentShield);
        }
        isShieldActive = false;
        currentCooldown = cooldown;
    }

    public void TakeShieldDamage(float damage)
    {
        shieldHealth -= damage;
        if (shieldHealth <= 0)
        {
            DestroyShield();
        }
    }
}

