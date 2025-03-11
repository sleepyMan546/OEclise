using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSkill : MonoBehaviour
{
    private WeaponSwitchDop weaponSwitch;
    private Hp playerHp;

    void Start()
    {
        weaponSwitch = GetComponentInParent<WeaponSwitchDop>();
        playerHp = GetComponent<Hp>();
        if (weaponSwitch == null)
        {
            Debug.LogError("WeaponSwitchDop not found!");
        }
        if (playerHp == null)
        {
            Debug.LogError("Hp script not found!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (weaponSwitch.GetCurrentWeapon() == "machineGun")
            {
                ActivateBarrierSkill();
            }
        }
    }

    void ActivateBarrierSkill()
    {
        if (playerHp != null)
        {
            bool activated = playerHp.ActivateBarrier();
            if (!activated)
            {
                Debug.Log("Cannot activate barrier. Cooldown remaining: " + playerHp.GetBarrierCooldown());
            }
        }
    }
}

