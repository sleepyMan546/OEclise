using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerDamage : MonoBehaviour
{
    public int damage = 10;
    public float damagePerSecond = 1f;
    [SerializeField] private LayerMask Ground;

    private bool isPlayerInContact = false;
    private Hp playerHp;
    private Rigidbody2D rigid;

    void Start()
    {
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.gameObject.CompareTag("Player"))
        {
            
            playerHp = collision.gameObject.GetComponent<Hp>();
            if (playerHp != null)
            {
                isPlayerInContact = true;
                StartCoroutine(DamageOverTime());
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInContact = false;
        }
    }

    IEnumerator DamageOverTime()
    {
        while (isPlayerInContact && playerHp != null) 
        {
            playerHp.TakeDamage(damage);
            yield return new WaitForSeconds(damagePerSecond);
        }
    }

}
