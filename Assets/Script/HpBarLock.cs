using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarLock : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;


    }

    void LateUpdate()
    {
        
        if (mainCamera != null)
        {
            transform.rotation = mainCamera.transform.rotation;
        }


    }
}
