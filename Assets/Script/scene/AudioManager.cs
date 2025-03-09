using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ���� namespace ���������ҹ SceneManager

public class Audiomanager : MonoBehaviour
{
    [Header("----audio source-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource GossipSource;

    [Header("----Scene Destruction Settings----")]
    [Tooltip("��� Scene Index ����ͧ������ AudioManager �١������������Ŵ Scene ���")]
    public int sceneIndexToDestroyOnLoad = -1; // ��˹� Scene Index ����ͧ���������� AudioManager, -1 ���¶֧�������㹷ء Scene

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // ����� AudioManager ���١��������������¹ Scene (¡��� Scene ����˹�)
    }

    private void Start()
    {
        musicSource.Play();
        GossipSource.Play();
        SceneManager.sceneLoaded += OnSceneLoaded; // ��Ѥ���Ҫԡ event ����� Scene ��Ŵ����
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ��Ǩ�ͺ��� Scene Index �����Ŵ�ç�Ѻ sceneIndexToDestroyOnLoad �������
        if (scene.buildIndex == sceneIndexToDestroyOnLoad && sceneIndexToDestroyOnLoad != -1)
        {
            Debug.Log("AudioManager �١��������ͧ�ҡ��Ŵ Scene Index: " + sceneIndexToDestroyOnLoad);
            Destroy(gameObject); // ����� GameObject �ͧ AudioManager
        }
    }

    // ¡��ԡ�����Ѥ���Ҫԡ event ����� GameObject �١����� ���ͻ�ͧ�ѹ memory leak
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}