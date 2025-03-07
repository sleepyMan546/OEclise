using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyDetector : MonoBehaviour
{
    [SerializeField] private GameObject targetGameObject; // GameObject ����ͧ��õ�Ǩ�ͺ
    [SerializeField] private int sceneIndexToLoad;     // Index �ͧ Scene ������Ŵ
    [SerializeField] private float delayTime = 1f;    // ����˹�ǧ (�Թҷ�)

    private bool sceneLoaded = false; // ����� Flag ���ͻ�ͧ�ѹ�����Ŵ Scene ���

    void Update()
    {
        if (targetGameObject != null)
        {
            // GameObject �ѧ����� Scene, �ӧҹ����
        }
        else if (!sceneLoaded) // GameObject �١��������� ����ѧ�������Ŵ Scene
        {
            sceneLoaded = true; // ��� Flag ���ͻ�ͧ�ѹ�����Ŵ���
            StartCoroutine(LoadSceneWithDelay()); // ����� Coroutine ������Ŵ Scene ����� Delay
        }
    }

    IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(delayTime); // �͵�����ҷ���˹�
        SceneManager.LoadScene(sceneIndexToLoad);  // ��Ŵ Scene ��� Index
    }
}