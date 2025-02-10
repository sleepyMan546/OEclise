using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gunPrefab; // ลาก Gun Prefab มาใส่ในช่องนี้ใน Inspector
    public GameObject bowPrefab;
    public Dictionary<KeyCode, GameObject> weapons = new Dictionary<KeyCode, GameObject>();
    public Transform point;
    private Weapon currentWeapon;
    private GameObject currentWeaponObject;

    public void SetWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
        Debug.Log("เปลี่ยนอาวุธเป็น: " + currentWeapon.GetType().Name);
    }

    void Start()
    {

        GameObject initialWeapon = Instantiate(gunPrefab, point.position, point.rotation);
        weapons.Add(KeyCode.Alpha1, initialWeapon);
        SetWeapon(initialWeapon);  // ตั้งค่าอาวุธเริ่มต้น
        currentWeaponObject = initialWeapon;

        // Instantiate ธนู และเก็บไว้ใน Dictionary แต่ *ไม่ activate*
        GameObject bow = Instantiate(bowPrefab, point.position, point.rotation);
        weapons.Add(KeyCode.Alpha2, bow);
        bow.SetActive(false); // ซ่อนธนูไว้ก่อน



    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(KeyCode.Alpha1, gunPrefab);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(KeyCode.Alpha2, bowPrefab);
        }

    }
    void SetWeapon(GameObject weaponObject)
    {
        currentWeaponObject = weaponObject;
    }
    void SwitchWeapon(KeyCode key, GameObject prefab)
    {
        if (weapons.ContainsKey(key))
        {
            if (weapons[key] != currentWeaponObject)
            {
                if (currentWeaponObject != null) currentWeaponObject.SetActive(false);
                GameObject newWeapon = weapons[key];
                newWeapon.SetActive(true); // เปิดใช้งานอาวุธใหม่
                SetWeapon(newWeapon);
                currentWeaponObject = newWeapon;
            }
        }
        else
        {
            // ไม่ควรเข้ามาใน else block นี้แล้ว เพราะอาวุธทุกชนิดถูกสร้างใน Start() แล้ว
            Debug.LogError("Weapon not found in dictionary: " + key); // แสดง error ถ้า key ไม่ถูกต้อง
        }
    }
}
