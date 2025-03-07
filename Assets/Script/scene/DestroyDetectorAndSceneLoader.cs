using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyDetector : MonoBehaviour
{
    [SerializeField] private GameObject targetGameObject; // GameObject ที่ต้องการตรวจสอบ
    [SerializeField] private int sceneIndexToLoad;     // Index ของ Scene ที่จะโหลด
    [SerializeField] private float delayTime = 1f;    // เวลาหน่วง (วินาที)

    private bool sceneLoaded = false; // ตัวแปร Flag เพื่อป้องกันการโหลด Scene ซ้ำ

    void Update()
    {
        if (targetGameObject != null)
        {
            // GameObject ยังอยู่ใน Scene, ทำงานปกติ
        }
        else if (!sceneLoaded) // GameObject ถูกทำลายแล้ว และยังไม่ได้โหลด Scene
        {
            sceneLoaded = true; // ตั้ง Flag เพื่อป้องกันการโหลดซ้ำ
            StartCoroutine(LoadSceneWithDelay()); // เริ่ม Coroutine เพื่อโหลด Scene พร้อม Delay
        }
    }

    IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(delayTime); // รอตามเวลาที่กำหนด
        SceneManager.LoadScene(sceneIndexToLoad);  // โหลด Scene ตาม Index
    }
}