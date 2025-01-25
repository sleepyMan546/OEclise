using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 3f; // ความเร็วการเคลื่อนที่ของศัตรู
    private Transform player; // ตำแหน่งของผู้เล่น
    private Rigidbody2D rb;

    private bool faceleft = true;
    public Transform firePoint;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // หาตำแหน่งของผู้เล่น
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized; // คำนวณทิศทางไปยังผู้เล่น
            rb.velocity = direction * moveSpeed; // เคลื่อนที่ไปยังผู้เล่น

            // ตรวจสอบทิศทางและ Flip ศัตรู
            if (direction.x > 0 && faceleft) // ถ้าศัตรูเคลื่อนที่ไปทางขวาและหันไปทางซ้าย
            {
                Flip();
            }
            else if (direction.x < 0 && !faceleft) // ถ้าศัตรูเคลื่อนที่ไปทางซ้ายและหันไปทางขวา
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        faceleft = !faceleft;
        transform.localRotation = Quaternion.Euler(0f, faceleft ? 0f : 180f, 0f);
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;


        //// หมุน Firepoint 180 องศารอบแกน Z
        //firePoint.localRotation = Quaternion.Euler(0f, 0f, faceleft ? 0f : 180f);
    }
}