using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab; // Prefab บอสที่คุณต้องการสร้าง (ลาก Prefab บอสมาใส่ใน Inspector)
    public Transform spawnPoint;   // ตำแหน่งที่จะสร้างบอส (ลาก Empty GameObject มาใส่ใน Inspector)
    public string playerTag = "Player"; // Tag ของ Player (ตั้งค่า Tag ให้ Player GameObject ใน Inspector)
    private bool hasSpawned = false; // ตัวแปรตรวจสอบว่าบอสถูกสร้างไปแล้วหรือยัง

    void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่า Trigger ที่เข้ามาชนเป็น Player และยังไม่เคยสร้างบอสมาก่อน
        if (!hasSpawned && other.gameObject.tag == playerTag)
        {
            SpawnBoss();
            hasSpawned = true; // ตั้งค่าว่าบอสถูกสร้างแล้ว เพื่อไม่ให้สร้างซ้ำ
        }
    }

    void SpawnBoss()
    {
        if (bossPrefab != null && spawnPoint != null)
        {
            // สร้างบอสที่ตำแหน่ง Spawn Point
            Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("Boss spawned!"); // ข้อความแสดงใน Console เมื่อบอสถูกสร้าง (เพื่อตรวจสอบ)
        }
        else
        {
            Debug.LogError("BossPrefab หรือ SpawnPoint ไม่ได้ถูกกำหนดใน Inspector!"); // ข้อความ Error หากไม่ได้ตั้งค่าใน Inspector
        }
    }
}