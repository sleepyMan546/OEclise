using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public GameObject bossGameObject; // GameObject ��� (�ҡ GameObject ��������� Inspector)
    public GameObject canvasToActivate; // Canvas GameObject ����ͧ����Դ��ҹ (�ҡ Canvas GameObject ������ Inspector)
    public string playerTag = "Player"; // Tag �ͧ Player
    private bool hasActivated = false; // ����õ�Ǩ�ͺ��Ҷ١�Դ��ҹ����������ѧ

    void OnTriggerEnter2D(Collider2D other)
    {
        // ��Ǩ�ͺ��� Trigger �������Ҫ��� Player ����ѧ������Դ��ҹ�ҡ�͹
        if (!hasActivated && other.gameObject.tag == playerTag)
        {
            ActivateBossAndCanvas(); // ���¡�ѧ��ѹ�������Դ��ҹ��駺����� Canvas
            hasActivated = true;
        }
    }

    void ActivateBossAndCanvas()
    {
        if (bossGameObject != null)
        {
            // �Դ��ҹ GameObject �ͧ��� (���� Component ��)
            bossGameObject.SetActive(true);
            // �����Դ Component ੾�� (����͹���):
            // BossComponent bossComponent = bossGameObject.GetComponent<BossComponent>();
            // if (bossComponent != null)
            // {
            //     bossComponent.enabled = true;
            // }
            // else
            // {
            //     Debug.LogError("��辺 Component 'BossComponent' �� BossGameObject!");
            //     return;
            // }
            Debug.Log("Boss activated!");
        }
        else
        {
            Debug.LogError("BossGameObject �����١��˹�� Inspector!");
        }

        // �Դ��ҹ Canvas GameObject ��Ҷ١��˹����
        if (canvasToActivate != null)
        {
            canvasToActivate.SetActive(true);
            Debug.Log("Canvas activated!");
        }
        else
        {
            Debug.LogWarning("CanvasGameObject �����١��˹�� Inspector, ��������Դ��ҹ Canvas."); // Warning ᷹ Error ���� Canvas �Ҩ�������������
        }
    }
}