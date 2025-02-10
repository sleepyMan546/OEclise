using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, Weapon
{

    private GameObject bow;
    public void Attack()
    {
        Debug.Log("Bow Attack");
    }
    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKey(KeyCode.Q))
        {
            Attack();
        }
    }
}
