using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHoming : MonoBehaviour
{
    public Transform firePoint;
    public float bulletSpeed = 5f;
    public float fireRate = 2f;
    public float detectionRange = 15f;

    private Transform player;
    private float nextFireTime;

    [SerializeField] private AudioSource homingShootingSoundSource; // ��������� AudioSource ����Ѻ���§�ԧ Homing

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ��Ǩ�ͺ������˹� AudioSource ����Ѻ���§�ԧ Homing � Inspector �����ѧ
        if (homingShootingSoundSource == null)
        {
            Debug.LogError("������˹� AudioSource ����Ѻ���§�ԧ Homing � Inspector! ��س��ҡ GameObject ����� AudioSource �����㹪�ͧ Homing Shooting Sound Source � Inspector �ͧʤ�Ի�� EnemyHoming");
        }
    }


    public void HomingShoot(Transform target, GameObject homingBulletPrefab)
    {
        Vector2 directionToPlayer = (target.position - transform.position).normalized;

        GameObject bullet = Instantiate(homingBulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.velocity = directionToPlayer * bulletSpeed;
        Debug.Log("Shoot");

        // ������§�ԧ Homing (��� AudioSource �١��˹����)
        if (homingShootingSoundSource != null)
        {
            homingShootingSoundSource.Play();
        }
    }
}
