using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderWithDelay : MonoBehaviour
{
    public int sceneIndexToLoad; // ��˹� Scene Index ����ͧ�����Ŵ� Inspector
    public float delayInSeconds = 1f; // ��˹�˹�ǧ���� (�Թҷ�) � Inspector

    private bool isPlayerInside = false; // ���������Ҽ���������� Trigger �����ѧ

    void OnTriggerEnter2D(Collider2D other)
    {
        // ��Ǩ�ͺ��� Collider �������Ҫ��� Player ������� (�Ҩ���� Tag ���� Component ����)
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = true;
            StartCoroutine(LoadSceneAfterDelay()); // ����� Coroutine �����������Ŵ Scene
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // ����ͼ������͡�ҡ Trigger �����ش�����Ŵ Scene (����ѧ�����Ŵ) ������� isPlayerInside
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = false;
            StopCoroutine(LoadSceneAfterDelay()); // ��ش Coroutine �ҡ�ѧ�ӧҹ����
        }
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds); // �͵�����ҷ���˹�

        if (isPlayerInside) // ��Ǩ�ͺ�ա������Ҽ������ѧ����� Trigger (���ͤ�����ʹ���)
        {
            SceneManager.LoadSceneAsync(sceneIndexToLoad); // ��Ŵ Scene Ẻ Asynchronous (�����������ҧ)
        }
    }
}
