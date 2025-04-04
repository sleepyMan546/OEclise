using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Setting_Dop : MonoBehaviour
{
    public Button setToFullHDButton; 
    public Button resetButton; 
    public Button backButton; 

    private Resolution defaultResolution; 

    void Start()
    {
        
        defaultResolution = Screen.currentResolution;

        setToFullHDButton.onClick.AddListener(SetToFullHD);
        resetButton.onClick.AddListener(ResetSettings);
        backButton.onClick.AddListener(BackToMenu);
    }

    void SetToFullHD()
    {
      
        Screen.SetResolution(1920, 1080, true);
        Debug.Log("Set Resolution to Full HD: 1920x1080, Fullscreen: True");
    }

    void ResetSettings()
    {
      
        Screen.SetResolution(defaultResolution.width, defaultResolution.height, Screen.fullScreen);
        Debug.Log($"Reset Resolution to: {defaultResolution.width}x{defaultResolution.height}, Fullscreen: {Screen.fullScreen}");
    }

    void BackToMenu()
    {
       
        SceneManager.LoadScene("MainMenu");
    }
}
