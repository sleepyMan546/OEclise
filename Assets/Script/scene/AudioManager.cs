using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // เพิ่ม namespace นี้เพื่อใช้งาน SceneManager

public class Audiomanager : MonoBehaviour
{
    [Header("----audio source-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource GossipSource;

    [Header("----Scene Destruction Settings----")]
    [Tooltip("ใส่ Scene Index ที่ต้องการให้ AudioManager ถูกทำลายเมื่อโหลด Scene เหล่านั้น (ใส่ได้หลาย Scene)")] // แก้ไข Tooltip
    public List<int> sceneIndicesToDestroyOnLoad = new List<int>(); // เปลี่ยนเป็น List<int>

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // ทำให้ AudioManager ไม่ถูกทำลายเมื่อเปลี่ยน Scene (ยกเว้น Scene ที่กำหนด)
    }

    private void Start()
    {
        musicSource.Play();
        GossipSource.Play();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ตรวจสอบว่า Scene Index ที่โหลดตรงกับ sceneIndicesToDestroyOnLoad หรือไม่
        if (sceneIndicesToDestroyOnLoad != null && sceneIndicesToDestroyOnLoad.Count > 0) // ตรวจสอบว่า List ไม่เป็น null และมีข้อมูล
        {
            foreach (int destroyIndex in sceneIndicesToDestroyOnLoad) // วนลูปตรวจสอบทุก Scene Index ใน List
            {
                if (scene.buildIndex == destroyIndex)
                {
                    Debug.Log("AudioManager ถูกทำลายเนื่องจากโหลด Scene Index: " + destroyIndex);
                    Destroy(gameObject); // ทำลาย GameObject ของ AudioManager
                    return; // ออกจากฟังก์ชันหลังจากทำลายแล้ว (เพื่อไม่ให้วนลูปต่อ)
                }
            }
        }
    }

    // ยกเลิกการสมัครสมาชิก event เมื่อ GameObject ถูกทำลาย เพื่อป้องกัน memory leak
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}