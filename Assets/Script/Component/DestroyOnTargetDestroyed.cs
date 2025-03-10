using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTargetDestroyed : MonoBehaviour
{
    [Tooltip("GameObject เป้าหมายที่ต้องการตรวจสอบการถูกทำลาย")]
    public GameObject targetGameObject;

    void Update()
    {
        // ตรวจสอบว่า targetGameObject เป็น null หรือไม่
        // ถ้าเป็น null แสดงว่า GameObject นั้นถูกทำลายไปแล้ว
        if (targetGameObject == null)
        {
            Debug.Log("GameObject เป้าหมายถูกทำลายแล้ว! ทำลาย GameObject นี้ด้วย.");
            Destroy(gameObject); // ทำลาย GameObject ที่สคริปต์นี้ติดอยู่
        }
    }
}
