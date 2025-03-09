using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GodMode : MonoBehaviour
{
    public static bool isGodMode = false; 
    private bool isSelectingScene = false;
    private List<string> sceneNames = new List<string> { "Tutorial", 
    "Tutorial_1" ,
        "Map_2",
    "Map_3",
    "Map_4_boss",
    "boss1_intro",
    "Boss1_dead",
    "Map5", };
    private int selectedSceneIndex = 0;

    void Update()
    {
        // กด G เพื่อเปิด/ปิด God Mode
        if (Input.GetKeyDown(KeyCode.G))
        {
            isGodMode = !isGodMode;
            Debug.Log("God Mode: " + (isGodMode ? "ON" : "OFF"));
        }

       
        if (Input.GetKeyDown(KeyCode.L))
        {
            isSelectingScene = !isSelectingScene;
        }

        
        if (isSelectingScene)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) 
            {
                selectedSceneIndex = (selectedSceneIndex - 1 + sceneNames.Count) % sceneNames.Count;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) 
            {
                selectedSceneIndex = (selectedSceneIndex + 1) % sceneNames.Count;
            }
            else if (Input.GetKeyDown(KeyCode.Return)) 
            {
                SceneManager.LoadScene(sceneNames[selectedSceneIndex]);
                isSelectingScene = false;
            }
        }
    }

    void OnGUI()
    {
        if (isSelectingScene)
        {
            GUI.Box(new Rect(10, 10, 200, 150), "เลือก Scene");
            for (int i = 0; i < sceneNames.Count; i++)
            {
                if (i == selectedSceneIndex)
                    GUI.color = Color.yellow;
                else
                    GUI.color = Color.white;

                GUI.Label(new Rect(20, 40 + (i * 20), 200, 20), sceneNames[i]);
            }
        }
    }
}
