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
    [Tooltip("��� Scene Index ����ͧ������ AudioManager �١������������Ŵ Scene ����ҹ�� (��������� Scene)")] // ��� Tooltip
    public List<int> sceneIndicesToDestroyOnLoad = new List<int>(); // ����¹�� List<int>

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // ����� AudioManager ���١��������������¹ Scene (¡��� Scene ����˹�)
    }

    private void Start()
    {
        musicSource.Play();
        GossipSource.Play();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ��Ǩ�ͺ��� Scene Index �����Ŵ�ç�Ѻ sceneIndicesToDestroyOnLoad �������
        if (sceneIndicesToDestroyOnLoad != null && sceneIndicesToDestroyOnLoad.Count > 0) // ��Ǩ�ͺ��� List ����� null ����բ�����
        {
            foreach (int destroyIndex in sceneIndicesToDestroyOnLoad) // ǹ�ٻ��Ǩ�ͺ�ء Scene Index � List
            {
                if (scene.buildIndex == destroyIndex)
                {
                    Debug.Log("AudioManager �١��������ͧ�ҡ��Ŵ Scene Index: " + destroyIndex);
                    Destroy(gameObject); // ����� GameObject �ͧ AudioManager
                    return; // �͡�ҡ�ѧ��ѹ��ѧ�ҡ��������� (����������ǹ�ٻ���)
                }
            }
        }
    }

    // ¡��ԡ�����Ѥ���Ҫԡ event ����� GameObject �١����� ���ͻ�ͧ�ѹ memory leak
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}