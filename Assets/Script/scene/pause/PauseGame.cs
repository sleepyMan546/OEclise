using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas; // ช่องสำหรับลาก Canvas Pause มาใส่ใน Inspector

    private bool isPaused = false; // ตัวแปรเก็บสถานะว่าเกม Pause หรือไม่

    void Start()
    {
        // ตรวจสอบว่าได้กำหนด Pause Canvas ใน Inspector หรือยัง
        if (pauseCanvas == null)
        {
            Debug.LogError("ไม่ได้กำหนด Pause Canvas ใน Inspector! กรุณาลาก Canvas Pause มาใส่ในช่อง Pause Canvas ใน Inspector ของสคริปต์ PauseGame");
            return; // หยุดการทำงานของสคริปต์หากไม่ได้กำหนด Canvas
        }

        // ซ่อน Pause Canvas ตอนเริ่มเกม
        pauseCanvas.SetActive(false);
    }

    void Update()
    {
        // ตรวจสอบการกดปุ่ม Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(); // เรียกฟังก์ชัน TogglePause เมื่อกด Esc
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused; // สลับสถานะ Pause (ถ้าเป็น true เปลี่ยนเป็น false, ถ้าเป็น false เปลี่ยนเป็น true)

        if (isPaused)
        {
            PauseGameFunction(); // เรียกฟังก์ชัน Pause เกม
        }
        else
        {
            ResumeGameFunction(); // เรียกฟังก์ชัน Resume เกม
        }
    }

    void PauseGameFunction()
    {
        Time.timeScale = 0f; // หยุดเวลาในเกม (Pause)
        pauseCanvas.SetActive(true); // เปิด Pause Canvas
        Debug.Log("Game Paused"); // แสดงข้อความใน Console (สำหรับ Debug)
    }

    void ResumeGameFunction()
    {
        Time.timeScale = 1f; // คืนค่าเวลาให้เป็นปกติ (Unpause)
        pauseCanvas.SetActive(false); // ปิด Pause Canvas
        Debug.Log("Game Resumed"); // แสดงข้อความใน Console (สำหรับ Debug)
    }
}
