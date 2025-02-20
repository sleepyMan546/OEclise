using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlaform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 1f;

    void Update()
    {
        transform.position = Vector3.Lerp(pointA.position, pointB.position, Mathf.PingPong(Time.time * speed, 1f));
    }
}
