﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
        
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // รับตำแหน่งเมาส์
            Vector2 direction = mousePosition - transform.position; // คำนวณทิศทางจากแขนไปยังเมาส์
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // คำนวณมุม
            transform.rotation = Quaternion.Euler(0, 0, angle);
        if (mousePosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1); // พลิกตัวละคร
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
