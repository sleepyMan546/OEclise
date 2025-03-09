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
    [Tooltip("ใส่ Scene Index ที่ต้องการให้ AudioManager ถูกทำลายเมื่อโหลด Scene นั้น")]
    public int sceneIndexToDestroyOnLoad = -1; // กำหนด Scene Index ที่ต้องการให้ทำลาย AudioManager, -1 หมายถึงไม่ทำลายในทุก Scene

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // ทำให้ AudioManager ไม่ถูกทำลายเมื่อเปลี่ยน Scene (ยกเว้น Scene ที่กำหนด)
    }

    private void Start()
    {
        musicSource.Play();
        GossipSource.Play();
        SceneManager.sceneLoaded += OnSceneLoaded; // สมัครสมาชิก event เมื่อ Scene โหลดเสร็จ
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ตรวจสอบว่า Scene Index ที่โหลดตรงกับ sceneIndexToDestroyOnLoad หรือไม่
        if (scene.buildIndex == sceneIndexToDestroyOnLoad && sceneIndexToDestroyOnLoad != -1)
        {
            Debug.Log("AudioManager ถูกทำลายเนื่องจากโหลด Scene Index: " + sceneIndexToDestroyOnLoad);
            Destroy(gameObject); // ทำลาย GameObject ของ AudioManager
        }
    }

    // ยกเลิกการสมัครสมาชิก event เมื่อ GameObject ถูกทำลาย เพื่อป้องกัน memory leak
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}