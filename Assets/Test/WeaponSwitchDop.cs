using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchDop : MonoBehaviour
{
    public GameObject pistol;
    public GameObject shotgun;

    
    private Dictionary<string, GameObject> weapons;

    
    private string currentWeapon = "pistol";

    void Start()
    {
       
        weapons = new Dictionary<string, GameObject>
        {
            { "pistol", pistol },
            { "shotgun", shotgun }
        };

        
        UpdateWeaponVisibility();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon("pistol");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon("shotgun");
        }
    }

    void SwitchWeapon(string weaponName)
    {
        if (weapons.ContainsKey(weaponName) && currentWeapon != weaponName)
        {
            currentWeapon = weaponName;
            UpdateWeaponVisibility();
        }
    }

    void UpdateWeaponVisibility()
    {
       
        foreach (var weapon in weapons.Values)
        {
            weapon.SetActive(false);
        }

       
        weapons[currentWeapon].SetActive(true);
    }
}
