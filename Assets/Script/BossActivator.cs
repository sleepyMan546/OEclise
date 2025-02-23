using UnityEngine;

public class BossActivator : MonoBehaviour
{
    public GameObject bossGameObject; 
    public GameObject canvasToActivate; 
    public string playerTag = "Player"; 
    private bool hasActivated = false; 

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (!hasActivated && other.gameObject.tag == playerTag)
        {
            ActivateBossAndCanvas(); 
            hasActivated = true;
        }
    }

    void ActivateBossAndCanvas()
    {
        if (bossGameObject != null)
        {
          
            bossGameObject.SetActive(true);
         
            Debug.Log("Boss activated!");
        }
        else
        {
            Debug.LogError("BossGameObject cannot active¹ Inspector!");
        }

       
        if (canvasToActivate != null)
        {
            canvasToActivate.SetActive(true);
            Debug.Log("Canvas activated!");
        }
        else
        {
            Debug.LogWarning("CanvasGameObject cannot active");
        }
    }
}