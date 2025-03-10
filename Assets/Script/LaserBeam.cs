using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private float damage = 20f; 
    [SerializeField] private float lifetime = 1f; 
    [SerializeField] private float beamLength = 5f; 
    [SerializeField] private LineRenderer lineRenderer;

    void Start()
    {
        // ตั้งค่า LineRenderer
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2; 
            lineRenderer.startWidth = 0.2f; 
            lineRenderer.endWidth = 0.2f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default")); 
            lineRenderer.startColor = Color.red; 
            lineRenderer.endColor = Color.red;

          
            UpdateLaserPosition();
        }

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (lineRenderer != null)
        {
            UpdateLaserPosition();
        }
    }

   
    void UpdateLaserPosition()
    {
        if (lineRenderer != null)
        {
        
            Vector3 startPoint = transform.position;

        
            Vector3 direction = transform.right; 
            Vector3 endPoint = startPoint + direction * beamLength;

            
            RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, beamLength);
            if (hit.collider != null && hit.collider.CompareTag("Player") == false)
            {
                endPoint = hit.point; 
            }

            
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, endPoint);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Hp playerHp = collision.GetComponent<Hp>();
            if (playerHp != null)
            {
                playerHp.TakeDamage((int)damage);
                Debug.Log("Player hit by laser! Damage: " + damage);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
    
        if (collision.CompareTag("Player"))
        {
            Hp playerHp = collision.GetComponent<Hp>();
            if (playerHp != null)
            {
                playerHp.TakeDamage((int)damage);
                Debug.Log("Player taking continuous laser damage!");
            }
        }
    }
}
