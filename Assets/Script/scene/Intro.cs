using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public float wait_time = 5f;
    public int scene_index = 5;
    void Start()
    {
        StartCoroutine(wait_for_intro());
    }
    IEnumerator wait_for_intro()
    {
        yield return new WaitForSeconds(wait_time);
        SceneManager.LoadScene(scene_index);
        
    }

}
