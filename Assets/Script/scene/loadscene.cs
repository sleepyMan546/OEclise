using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadscene : MonoBehaviour
{
    public int sceneIndex;
    public float delay;
    public void PlayGame()
    {
        StartCoroutine(LoadSceneWithDelay(sceneIndex, delay));
    }

    IEnumerator LoadSceneWithDelay(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadSceneAsync(sceneIndex);
    }

}
