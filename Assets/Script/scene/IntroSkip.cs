using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroSkip : MonoBehaviour
{
    [Tooltip("Scene Index ����ͧ�����Ŵ����͡� Spacebar")]
    public int sceneIndexToLoad;

    [Tooltip("��������˹�ǧ (�Թҷ�) ��͹��Ŵ Scene")]
    public float delayTime = 1f; // ��˹�����������˹�ǧ������ 1 �Թҷ�

    private bool isSkipActive = false; // ����� Flag ���ͻ�ͧ�ѹ������¡ Coroutine ���

    void Update()
    {
        // ��Ǩ�ͺ��һ��� Spacebar �١��ŧ ����ѧ���������� Coroutine ��� Skip
        if (Input.GetKeyDown(KeyCode.Space) && !isSkipActive)
        {
            isSkipActive = true; 
            StartCoroutine(LoadSceneWithDelay());
        }
    }

    IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(delayTime); // ˹�ǧ���ҵ������˹�

        // ��ѧ�ҡ˹�ǧ����������� �����Ŵ Scene ����˹�
        SceneManager.LoadSceneAsync(sceneIndexToLoad); // ��Ŵ Scene Ẻ Asynchronous (�����������ҧ)
    }
}
