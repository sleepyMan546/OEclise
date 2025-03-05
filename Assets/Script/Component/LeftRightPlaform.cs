using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightPlaform : MonoBehaviour
{
    public float speed = 5f;
    public Transform child;
    public Transform parent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("platform"))
        {
            speed =-speed;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            child.transform.SetParent(parent);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            child.transform.SetParent(null); ;
        }
    }
    private void Update()
    {
        transform.Translate(speed * Time.deltaTime,0,0);
    }
}
