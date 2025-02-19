using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointSystem checkpointSystem;

    void Start()
    {
        checkpointSystem = FindObjectOfType<CheckpointSystem>(); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            checkpointSystem.SetCheckpoint(transform.position);
            Debug.Log("Checkpoint Saved: " + transform.position);
        }
    }
}