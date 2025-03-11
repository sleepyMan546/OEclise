using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchDop : MonoBehaviour
{
    public GameObject pistol;
    public GameObject shotgun;
    public GameObject machineGun;

    [SerializeField] private AudioSource pistolSwitchSoundSource; 
    [SerializeField] private AudioSource shotgunSwitchSoundSource;
    [SerializeField] private AudioSource mechineGunSwitchSoundSource;
    private Dictionary<string, GameObject> weapons;
    private string currentWeapon = "pistol";

    void Start()
    {
        weapons = new Dictionary<string, GameObject>
        {
            { "pistol", pistol },
            { "shotgun", shotgun }
            ,{ "machineGun", machineGun }
        };

        UpdateWeaponVisibility();

        
        if (pistolSwitchSoundSource == null)
        {
            Debug.LogError("No audio sourch");
        }
        if (shotgunSwitchSoundSource == null)
        {
            Debug.LogError("No audio sourch");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            SwitchWeapon("pistol");
        }
        else if (Input.GetKeyDown(KeyCode.W)) 
        {
            SwitchWeapon("shotgun");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchWeapon("machineGun");
        }


    }

    void SwitchWeapon(string weaponName)
    {
        if (weapons.ContainsKey(weaponName) && currentWeapon != weaponName)
        {
            currentWeapon = weaponName;
            UpdateWeaponVisibility();

            // เล่นเสียงสลับอาวุธตาม weaponName
            if (weaponName == "pistol")
            {
                if (pistolSwitchSoundSource != null)
                {
                    pistolSwitchSoundSource.Play();
                }
            }
            else if (weaponName == "shotgun")
            {
                if (shotgunSwitchSoundSource != null)
                {
                    shotgunSwitchSoundSource.Play();
                }
            }
            else if (weaponName == "machineGun")
            {
                if (mechineGunSwitchSoundSource != null)
                {
                    mechineGunSwitchSoundSource.Play();
                }
            }
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

    public string GetCurrentWeapon()
    {
        return currentWeapon;
    }
}
