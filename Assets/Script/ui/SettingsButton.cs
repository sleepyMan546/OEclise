using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    public Canvas settingsCanvas; // ช่องสำหรับลาก Canvas Settings มาใส่ใน Inspector

    void Start()
    {
        // ตรวจสอบว่า Canvas ถูกกำหนดค่าไว้ใน Inspector หรือยัง
        if (settingsCanvas == null)
        {
            Debug.LogError("ไม่ได้กำหนด Canvas Settings ให้กับปุ่ม Settings!");
            enabled = false; // ปิดการทำงานของสคริปต์นี้หากไม่มี Canvas
            return;
        }

        // ซ่อน Canvas Settings ในตอนเริ่มต้น (ถ้าคุณต้องการให้มันปิดอยู่ตอนเริ่มเกม)
        settingsCanvas.gameObject.SetActive(false);
    }

    public void ToggleSettingsCanvas()
    {
        // สลับสถานะการเปิด/ปิดของ Canvas Settings
        settingsCanvas.gameObject.SetActive(!settingsCanvas.gameObject.activeSelf);
    }
}
