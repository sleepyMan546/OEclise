using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, Weapon
{
     private GameObject gun;
    public void Attack()
    {
        Debug.Log("Gun Attack");
    }
    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey(KeyCode.Q))
        {
            Attack();
        }
    }
}
