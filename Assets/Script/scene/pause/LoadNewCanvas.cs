using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNewCanvas : MonoBehaviour
{
    [SerializeField] private GameObject canvasPrefabToLoad; // ��ͧ����Ѻ�ҡ Canvas Prefab ������ Inspector
    [SerializeField] private Transform canvasParent; // (Optional) ��ͧ����Ѻ��˹� Parent ��� Canvas ���� (�������������ҧ� Root)

    public void LoadCanvasButtonPress() // �ѧ��ѹ���ж١���¡����ͻ����١��
    {
        if (canvasPrefabToLoad != null)
        {
            // ���ҧ�Թ�ᵹ��ͧ Canvas Prefab
            GameObject newCanvasInstance = Instantiate(canvasPrefabToLoad);

            // (Optional) ��˹� Parent ��� Canvas ���� ����ա�á�˹� canvasParent ���
            if (canvasParent != null)
            {
                newCanvasInstance.transform.SetParent(canvasParent, false); // SetParent ���������¹ Scale ��� Rotation
            }

            Debug.Log("Canvas ����١��Ŵ����!"); // �ʴ���ͤ���� Console (����Ѻ Debug)
        }
        else
        {
            Debug.LogError("������˹� Canvas Prefab � Inspector! ��س��ҡ Canvas Prefab �����㹪�ͧ Canvas Prefab To Load � Inspector �ͧʤ�Ի�� LoadNewCanvas");
        }
    }
}
