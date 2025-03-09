using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    private void Awake()
    {
        
    }
    [SerializeField] GameObject pauseMenu;
    private bool isPaused = false; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) 
            {
                Resume(); 
            }
            else
            {
                Pause(); 
            }
            isPaused = !isPaused; 
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Game Paused (Esc)"); 
    }
    public void Home()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Game Resumed (Esc)"); 
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
