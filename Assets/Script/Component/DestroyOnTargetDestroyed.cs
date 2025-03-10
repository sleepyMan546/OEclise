using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTargetDestroyed : MonoBehaviour
{
    [Tooltip("GameObject ������·���ͧ��õ�Ǩ�ͺ��ö١�����")]
    public GameObject targetGameObject;

    void Update()
    {
        // ��Ǩ�ͺ��� targetGameObject �� null �������
        // ����� null �ʴ���� GameObject ��鹶١����������
        if (targetGameObject == null)
        {
            Debug.Log("GameObject ������¶١���������! ����� GameObject ������.");
            Destroy(gameObject); // ����� GameObject ���ʤ�Ի����Դ����
        }
    }
}
