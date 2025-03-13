using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    public Canvas settingsCanvas; // ��ͧ����Ѻ�ҡ Canvas Settings ������ Inspector

    void Start()
    {
        // ��Ǩ�ͺ��� Canvas �١��˹�������� Inspector �����ѧ
        if (settingsCanvas == null)
        {
            Debug.LogError("������˹� Canvas Settings ���Ѻ���� Settings!");
            enabled = false; // �Դ��÷ӧҹ�ͧʤ�Ի�����ҡ����� Canvas
            return;
        }

        // ��͹ Canvas Settings 㹵͹������� (��Ҥس��ͧ�������ѹ�Դ����͹�������)
        settingsCanvas.gameObject.SetActive(false);
    }

    public void ToggleSettingsCanvas()
    {
        // ��ѺʶҹС���Դ/�Դ�ͧ Canvas Settings
        settingsCanvas.gameObject.SetActive(!settingsCanvas.gameObject.activeSelf);
    }
}
