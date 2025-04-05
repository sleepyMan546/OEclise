using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Healing_Point : MonoBehaviour
{
    Hp hp;
    public GameObject healingpoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hp hpdop = collision.GetComponent<Hp>();
        if(hpdop != null)
        {
            hpdop.currentHealth = 500;
            Destroy(healingpoint);
        }
    }
}
