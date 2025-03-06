using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas; // ��ͧ����Ѻ�ҡ Canvas Pause ������ Inspector

    private bool isPaused = false; // �������ʶҹ������ Pause �������

    void Start()
    {
        // ��Ǩ�ͺ������˹� Pause Canvas � Inspector �����ѧ
        if (pauseCanvas == null)
        {
            Debug.LogError("������˹� Pause Canvas � Inspector! ��س��ҡ Canvas Pause �����㹪�ͧ Pause Canvas � Inspector �ͧʤ�Ի�� PauseGame");
            return; // ��ش��÷ӧҹ�ͧʤ�Ի���ҡ������˹� Canvas
        }

        // ��͹ Pause Canvas �͹�������
        pauseCanvas.SetActive(false);
    }

    void Update()
    {
        // ��Ǩ�ͺ��á����� Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(); // ���¡�ѧ��ѹ TogglePause ����͡� Esc
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused; // ��Ѻʶҹ� Pause (����� true ����¹�� false, ����� false ����¹�� true)

        if (isPaused)
        {
            PauseGameFunction(); // ���¡�ѧ��ѹ Pause ��
        }
        else
        {
            ResumeGameFunction(); // ���¡�ѧ��ѹ Resume ��
        }
    }

    void PauseGameFunction()
    {
        Time.timeScale = 0f; // ��ش������� (Pause)
        pauseCanvas.SetActive(true); // �Դ Pause Canvas
        Debug.Log("Game Paused"); // �ʴ���ͤ���� Console (����Ѻ Debug)
    }

    void ResumeGameFunction()
    {
        Time.timeScale = 1f; // �׹�����������繻��� (Unpause)
        pauseCanvas.SetActive(false); // �Դ Pause Canvas
        Debug.Log("Game Resumed"); // �ʴ���ͤ���� Console (����Ѻ Debug)
    }
}
