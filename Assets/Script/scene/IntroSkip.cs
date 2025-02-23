using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroSkip : MonoBehaviour
{
    [Tooltip("Scene Index ที่ต้องการโหลดเมื่อกด Spacebar")]
    public int sceneIndexToLoad;

    [Tooltip("ระยะเวลาหน่วง (วินาที) ก่อนโหลด Scene")]
    public float delayTime = 1f; // กำหนดค่าเริ่มต้นหน่วงเวลาเป็น 1 วินาที

    private bool isSkipActive = false; // ตัวแปร Flag เพื่อป้องกันการเรียก Coroutine ซ้ำ

    void Update()
    {
        // ตรวจสอบว่าปุ่ม Spacebar ถูกกดลง และยังไม่ได้เริ่ม Coroutine การ Skip
        if (Input.GetKeyDown(KeyCode.Space) && !isSkipActive)
        {
            isSkipActive = true; 
            StartCoroutine(LoadSceneWithDelay());
        }
    }

    IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(delayTime); // หน่วงเวลาตามที่กำหนด

        // หลังจากหน่วงเวลาเสร็จสิ้น ให้โหลด Scene ที่กำหนด
        SceneManager.LoadSceneAsync(sceneIndexToLoad); // โหลด Scene แบบ Asynchronous (ไม่ทำให้เกมค้าง)
    }
}
