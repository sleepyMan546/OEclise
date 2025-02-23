using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderWithDelay : MonoBehaviour
{
    public int sceneIndexToLoad; // กำหนด Scene Index ที่ต้องการโหลดใน Inspector
    public float delayInSeconds = 1f; // กำหนดหน่วงเวลา (วินาที) ใน Inspector

    private bool isPlayerInside = false; // ตัวแปรเช็คว่าผู้เล่นอยู่ใน Trigger หรือยัง

    void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่า Collider ที่เข้ามาชนเป็น Player หรือไม่ (อาจจะใช้ Tag หรือ Component อื่นๆ)
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = true;
            StartCoroutine(LoadSceneAfterDelay()); // เริ่ม Coroutine เพื่อรอและโหลด Scene
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // เมื่อผู้เล่นออกจาก Trigger ให้หยุดการโหลด Scene (ถ้ายังไม่โหลด) และรีเซ็ต isPlayerInside
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = false;
            StopCoroutine(LoadSceneAfterDelay()); // หยุด Coroutine หากยังทำงานอยู่
        }
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds); // รอตามเวลาที่กำหนด

        if (isPlayerInside) // ตรวจสอบอีกครั้งว่าผู้เล่นยังอยู่ใน Trigger (เพื่อความปลอดภัย)
        {
            SceneManager.LoadSceneAsync(sceneIndexToLoad); // โหลด Scene แบบ Asynchronous (ไม่ทำให้เกมค้าง)
        }
    }
}
