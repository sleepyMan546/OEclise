using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNewCanvas : MonoBehaviour
{
    [SerializeField] private GameObject canvasPrefabToLoad; // ช่องสำหรับลาก Canvas Prefab มาใส่ใน Inspector
    [SerializeField] private Transform canvasParent; // (Optional) ช่องสำหรับกำหนด Parent ให้ Canvas ใหม่ (ถ้าไม่ใส่จะสร้างใน Root)

    public void LoadCanvasButtonPress() // ฟังก์ชันนี้จะถูกเรียกเมื่อปุ่มถูกกด
    {
        if (canvasPrefabToLoad != null)
        {
            // สร้างอินสแตนซ์ของ Canvas Prefab
            GameObject newCanvasInstance = Instantiate(canvasPrefabToLoad);

            // (Optional) กำหนด Parent ให้ Canvas ใหม่ ถ้ามีการกำหนด canvasParent ไว้
            if (canvasParent != null)
            {
                newCanvasInstance.transform.SetParent(canvasParent, false); // SetParent โดยไม่เปลี่ยน Scale และ Rotation
            }

            Debug.Log("Canvas ใหม่ถูกโหลดแล้ว!"); // แสดงข้อความใน Console (สำหรับ Debug)
        }
        else
        {
            Debug.LogError("ไม่ได้กำหนด Canvas Prefab ใน Inspector! กรุณาลาก Canvas Prefab มาใส่ในช่อง Canvas Prefab To Load ใน Inspector ของสคริปต์ LoadNewCanvas");
        }
    }
}
