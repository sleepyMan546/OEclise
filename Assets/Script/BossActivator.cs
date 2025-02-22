using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public GameObject bossGameObject; // GameObject บอส (ลาก GameObject บอสมาใส่ใน Inspector)
    public GameObject canvasToActivate; // Canvas GameObject ที่ต้องการเปิดใช้งาน (ลาก Canvas GameObject มาใส่ใน Inspector)
    public string playerTag = "Player"; // Tag ของ Player
    private bool hasActivated = false; // ตัวแปรตรวจสอบว่าถูกเปิดใช้งานไปแล้วหรือยัง

    void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่า Trigger ที่เข้ามาชนเป็น Player และยังไม่เคยเปิดใช้งานมาก่อน
        if (!hasActivated && other.gameObject.tag == playerTag)
        {
            ActivateBossAndCanvas(); // เรียกฟังก์ชันใหม่ที่เปิดใช้งานทั้งบอสและ Canvas
            hasActivated = true;
        }
    }

    void ActivateBossAndCanvas()
    {
        if (bossGameObject != null)
        {
            // เปิดใช้งาน GameObject ของบอส (หรือ Component ใดๆ)
            bossGameObject.SetActive(true);
            // หรือเปิด Component เฉพาะ (เหมือนเดิม):
            // BossComponent bossComponent = bossGameObject.GetComponent<BossComponent>();
            // if (bossComponent != null)
            // {
            //     bossComponent.enabled = true;
            // }
            // else
            // {
            //     Debug.LogError("ไม่พบ Component 'BossComponent' บน BossGameObject!");
            //     return;
            // }
            Debug.Log("Boss activated!");
        }
        else
        {
            Debug.LogError("BossGameObject ไม่ได้ถูกกำหนดใน Inspector!");
        }

        // เปิดใช้งาน Canvas GameObject ถ้าถูกกำหนดไว้
        if (canvasToActivate != null)
        {
            canvasToActivate.SetActive(true);
            Debug.Log("Canvas activated!");
        }
        else
        {
            Debug.LogWarning("CanvasGameObject ไม่ได้ถูกกำหนดใน Inspector, ข้ามการเปิดใช้งาน Canvas."); // Warning แทน Error เพราะ Canvas อาจจะไม่จำเป็นเสมอไป
        }
    }
}