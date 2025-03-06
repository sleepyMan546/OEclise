using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchDop : MonoBehaviour
{
    public GameObject pistol;
    public GameObject shotgun;

    [SerializeField] private AudioSource pistolSwitchSoundSource; // AudioSource สำหรับเสียงสลับไปปืนพก
    [SerializeField] private AudioSource shotgunSwitchSoundSource; // AudioSource สำหรับเสียงสลับไปลูกซอง

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

        // ตรวจสอบว่าได้กำหนด AudioSource ใน Inspector หรือยัง
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
        if (Input.GetKeyDown(KeyCode.Q)) // เปลี่ยนจาก GetKey เป็น GetKeyDown เพื่อให้เล่นเสียงแค่ครั้งเดียวตอนกดปุ่ม
        {
            SwitchWeapon("pistol");
        }
        else if (Input.GetKeyDown(KeyCode.W)) // เปลี่ยนจาก GetKey เป็น GetKeyDown เพื่อให้เล่นเสียงแค่ครั้งเดียวตอนกดปุ่ม
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
