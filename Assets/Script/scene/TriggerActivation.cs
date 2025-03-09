using UnityEngine;
using System.Collections;

public class TriggerActivation : MonoBehaviour
{
    public GameObject targetGameObject;   // Game Object ����ͧ����Դ��ҹ
    public AudioSource targetAudioSource; // Audio Source ����ͧ������
    public float activationDuration = 3f; // �������ҷ�� Game Object �Դ���� (�Թҷ�)

    private bool hasBeenTriggered = false; // ����õ�Ǩ�ͺ��� Trigger �ӧҹ����������ѧ

    void Start()
    {
        // ��Ǩ�ͺ����ա�á�˹� Target Game Object ��� Audio Source �����ѧ
        if (targetGameObject == null)
        {
            Debug.LogError("TriggerActivation: Target Game Object �����١��˹�!");
            enabled = false; // �Դ Script ���������� Target Game Object
            return;
        }
        if (targetAudioSource == null)
        {
            Debug.LogError("TriggerActivation: Target Audio Source �����١��˹�!");
            enabled = false; // �Դ Script ���������� Target Audio Source
            return;
        }

        // ��Ǩ�ͺ��������� Game Object ������鹻Դ����
        if (targetGameObject.activeSelf)
        {
            Debug.LogWarning("TriggerActivation: Target Game Object ��è�������鹻Դ����!");
            targetGameObject.SetActive(false); // �Դ Game Object ����Դ����͹�������
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ��Ǩ�ͺ��� Object ��������� Trigger ��� Player ������� (�� Tag "Player")
        if (other.gameObject.CompareTag("Player"))
        {
            // ��Ǩ�ͺ��� Trigger �ѧ����·ӧҹ�ҡ�͹
            if (!hasBeenTriggered)
            {
                hasBeenTriggered = true; // ��駤����� Trigger �ӧҹ����

                // �Դ��ҹ Game Object
                targetGameObject.SetActive(true);

                // ��� Audio Source
                targetAudioSource.Play();

                // ���¡��ѧ��ѹ DeactivateGameObject ��ѧ�ҡ��ҹ� activationDuration �Թҷ�
                Invoke("DeactivateGameObject", activationDuration);
            }
        }
    }

    void DeactivateGameObject()
    {
        // �Դ��ҹ Game Object
        targetGameObject.SetActive(false);
    }

    // �ѧ��ѹ ResetTrigger ����Ѻ�óշ��س��ͧ������ Trigger ��Ѻ�ҷӧҹ���ա���� (�� ������)
    public void ResetTrigger()
    {
        hasBeenTriggered = false;
    }
}