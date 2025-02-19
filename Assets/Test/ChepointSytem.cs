using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    private Vector2 respawnPoint; 
    public Transform player;
    private Hp hp;

    void Start()
    {
       
        float x = PlayerPrefs.GetFloat("RespawnX", player.position.x);
        float y = PlayerPrefs.GetFloat("RespawnY", player.position.y);
        respawnPoint = new Vector2(x, y);
        hp = player.GetComponent<Hp>();
    }

    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        respawnPoint = newCheckpoint;

        
        PlayerPrefs.SetFloat("RespawnX", newCheckpoint.x);
        PlayerPrefs.SetFloat("RespawnY", newCheckpoint.y);
        PlayerPrefs.Save();
    }

    public void RespawnPlayer()
    {
        player.position = respawnPoint;
        hp.ResetHealth();
    }
}
