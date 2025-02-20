using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Razer : MonoBehaviour
{
    public LineRenderer lineRenderer;  
    public Transform laserOrigin;     
    public LayerMask laserMask;        
    public float maxLaserLength = 10f; 
    public int damage = 10;       
    public Hp hp;

    void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        lineRenderer.SetPosition(0, laserOrigin.position); 

        
        RaycastHit2D hit = Physics2D.Raycast(laserOrigin.position, transform.right, maxLaserLength, laserMask);

        Vector2 endPosition = laserOrigin.position + transform.right * maxLaserLength;

        if (hit.collider != null)
        {
            
            endPosition = hit.point;

           
            if (hit.collider.CompareTag("Player"))
            {
               hp = hit.collider.GetComponent<Hp>();
                if (hp != null)
                {
                    hp.TakeDamage(damage);
                }
            }
        }

       
        lineRenderer.SetPosition(1, endPosition);
    }
}
