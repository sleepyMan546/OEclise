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
    [SerializeField] private AudioSource machineGunSwitchSoundSource;

    private Dictionary<string, GameObject> weapons;
    private string[] weaponOrder = { "pistol", "shotgun", "machineGun" }; 
    private int currentWeaponIndex = 0;

    void Start()
    {
        weapons = new Dictionary<string, GameObject>
        {
            { "pistol", pistol },
            { "shotgun", shotgun },
            { "machineGun", machineGun }
        };

        UpdateWeaponVisibility();

        if (pistolSwitchSoundSource == null || shotgunSwitchSoundSource == null || machineGunSwitchSoundSource == null)
        {
            Debug.LogError("Missing audio source for one or more weapons!");
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

      
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    SwitchWeapon("pistol");
        //}
        //else if (Input.GetKeyDown(KeyCode.W))
        //{
        //    SwitchWeapon("shotgun");
        //}
        //else if (Input.GetKeyDown(KeyCode.E))
        //{
        //    SwitchWeapon("machineGun");
        //}



        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            SwitchWeapon("pistol");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            SwitchWeapon("shotgun");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) 
        {
            SwitchWeapon("machineGun");
        }

       
        if (Input.mouseScrollDelta.y > 0) 
        {
            SwitchToNextWeapon();
        }
        else if (Input.mouseScrollDelta.y < 0) 
        {
            SwitchToPreviousWeapon();
        }
    }

    void SwitchWeapon(string weaponName)
    {
        if (weapons.ContainsKey(weaponName) && weaponOrder[currentWeaponIndex] != weaponName)
        {
            currentWeaponIndex = System.Array.IndexOf(weaponOrder, weaponName);
            UpdateWeaponVisibility();

           
            PlaySwitchSound(weaponName);
        }
    }

    void SwitchToNextWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex + 1) % weaponOrder.Length;
        UpdateWeaponVisibility();
        PlaySwitchSound(weaponOrder[currentWeaponIndex]);
    }

    void SwitchToPreviousWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex - 1 + weaponOrder.Length) % weaponOrder.Length;
        UpdateWeaponVisibility();
        PlaySwitchSound(weaponOrder[currentWeaponIndex]);
    }

    void UpdateWeaponVisibility()
    {
        foreach (var weapon in weapons.Values)
        {
            weapon.SetActive(false);
        }
        weapons[weaponOrder[currentWeaponIndex]].SetActive(true);
    }

    void PlaySwitchSound(string weaponName)
    {
        if (weaponName == "pistol" && pistolSwitchSoundSource != null)
        {
            pistolSwitchSoundSource.Play();
        }
        else if (weaponName == "shotgun" && shotgunSwitchSoundSource != null)
        {
            shotgunSwitchSoundSource.Play();
        }
        else if (weaponName == "machineGun" && machineGunSwitchSoundSource != null)
        {
            machineGunSwitchSoundSource.Play();
        }
    }

    public string GetCurrentWeapon()
    {
        return weaponOrder[currentWeaponIndex];
    }
}
