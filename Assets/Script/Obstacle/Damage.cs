using System.Collections;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = 10; 
    public Hp hp; 
    private bool isPlayerInContact = false;
    public float damagepersecond = 1f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hp = collision.gameObject.GetComponent<Hp>(); 
            if (hp != null)
            {
                isPlayerInContact = true;
                StartCoroutine(DamageOverTime()); 
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInContact = false;
        }
    }

    IEnumerator DamageOverTime()
    {
        while (isPlayerInContact) 
        {
            hp.TakeDamage(damage);
            yield return new WaitForSeconds(damagepersecond);
        }
    }
}

