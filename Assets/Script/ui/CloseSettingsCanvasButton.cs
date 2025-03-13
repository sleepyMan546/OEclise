using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseSettingsCanvasButton : MonoBehaviour
{
    public Canvas settingsCanvasToClose; // ช่องสำหรับลาก Canvas ที่ต้องการปิด มาใส่ใน Inspector

    void Start()
    {
        // ตรวจสอบว่า Canvas ถูกกำหนดค่าไว้ใน Inspector หรือยัง
        if (settingsCanvasToClose == null)
        {
            Debug.LogError("ไม่ได้กำหนด Canvas ที่ต้องการปิด ให้กับปุ่มปิด Settings!");
            enabled = false; // ปิดการทำงานของสคริปต์นี้หากไม่มี Canvas
            return;
        }
    }

    public void CloseCanvas()
    {
        // ปิด Canvas ที่กำหนดไว้
        settingsCanvasToClose.gameObject.SetActive(false);
    }
}
