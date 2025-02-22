using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab; // Prefab ��ʷ��س��ͧ������ҧ (�ҡ Prefab ��������� Inspector)
    public Transform spawnPoint;   // ���˹觷������ҧ��� (�ҡ Empty GameObject ������ Inspector)
    public string playerTag = "Player"; // Tag �ͧ Player (��駤�� Tag ��� Player GameObject � Inspector)
    private bool hasSpawned = false; // ����õ�Ǩ�ͺ��Һ�ʶ١���ҧ����������ѧ

    void OnTriggerEnter2D(Collider2D other)
    {
        // ��Ǩ�ͺ��� Trigger �������Ҫ��� Player ����ѧ��������ҧ����ҡ�͹
        if (!hasSpawned && other.gameObject.tag == playerTag)
        {
            SpawnBoss();
            hasSpawned = true; // ��駤����Һ�ʶ١���ҧ���� �������������ҧ���
        }
    }

    void SpawnBoss()
    {
        if (bossPrefab != null && spawnPoint != null)
        {
            // ���ҧ��ʷ����˹� Spawn Point
            Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("Boss spawned!"); // ��ͤ����ʴ�� Console ����ͺ�ʶ١���ҧ (���͵�Ǩ�ͺ)
        }
        else
        {
            Debug.LogError("BossPrefab ���� SpawnPoint �����١��˹�� Inspector!"); // ��ͤ��� Error �ҡ������駤��� Inspector
        }
    }
}