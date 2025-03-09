using UnityEngine;
using System.Collections;

public class TriggerActivation : MonoBehaviour
{
    public GameObject targetGameObject;   // Game Object ที่ต้องการเปิดใช้งาน
    public AudioSource targetAudioSource; // Audio Source ที่ต้องการเล่น
    public float activationDuration = 3f; // ระยะเวลาที่ Game Object เปิดอยู่ (วินาที)

    private bool hasBeenTriggered = false; // ตัวแปรตรวจสอบว่า Trigger ทำงานไปแล้วหรือยัง

    void Start()
    {
        // ตรวจสอบว่ามีการกำหนด Target Game Object และ Audio Source หรือยัง
        if (targetGameObject == null)
        {
            Debug.LogError("TriggerActivation: Target Game Object ไม่ได้ถูกกำหนด!");
            enabled = false; // ปิด Script นี้ถ้าไม่มี Target Game Object
            return;
        }
        if (targetAudioSource == null)
        {
            Debug.LogError("TriggerActivation: Target Audio Source ไม่ได้ถูกกำหนด!");
            enabled = false; // ปิด Script นี้ถ้าไม่มี Target Audio Source
            return;
        }

        // ตรวจสอบให้แน่ใจว่า Game Object เริ่มต้นปิดอยู่
        if (targetGameObject.activeSelf)
        {
            Debug.LogWarning("TriggerActivation: Target Game Object ควรจะเริ่มต้นปิดอยู่!");
            targetGameObject.SetActive(false); // ปิด Game Object ถ้าเปิดอยู่ตอนเริ่มต้น
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่า Object ที่เข้ามาใน Trigger คือ Player หรือไม่ (ใช้ Tag "Player")
        if (other.gameObject.CompareTag("Player"))
        {
            // ตรวจสอบว่า Trigger ยังไม่เคยทำงานมาก่อน
            if (!hasBeenTriggered)
            {
                hasBeenTriggered = true; // ตั้งค่าว่า Trigger ทำงานแล้ว

                // เปิดใช้งาน Game Object
                targetGameObject.SetActive(true);

                // เล่น Audio Source
                targetAudioSource.Play();

                // เรียกใช้ฟังก์ชัน DeactivateGameObject หลังจากผ่านไป activationDuration วินาที
                Invoke("DeactivateGameObject", activationDuration);
            }
        }
    }

    void DeactivateGameObject()
    {
        // ปิดใช้งาน Game Object
        targetGameObject.SetActive(false);
    }

    // ฟังก์ชัน ResetTrigger สำหรับกรณีที่คุณต้องการให้ Trigger กลับมาทำงานได้อีกครั้ง (เช่น รีเซ็ตเกม)
    public void ResetTrigger()
    {
        hasBeenTriggered = false;
    }
}