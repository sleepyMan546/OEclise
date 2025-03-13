using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseSettingsCanvasButton : MonoBehaviour
{
    public Canvas settingsCanvasToClose; // ��ͧ����Ѻ�ҡ Canvas ����ͧ��ûԴ ������ Inspector

    void Start()
    {
        // ��Ǩ�ͺ��� Canvas �١��˹�������� Inspector �����ѧ
        if (settingsCanvasToClose == null)
        {
            Debug.LogError("������˹� Canvas ����ͧ��ûԴ ���Ѻ�����Դ Settings!");
            enabled = false; // �Դ��÷ӧҹ�ͧʤ�Ի�����ҡ����� Canvas
            return;
        }
    }

    public void CloseCanvas()
    {
        // �Դ Canvas ����˹����
        settingsCanvasToClose.gameObject.SetActive(false);
    }
}
