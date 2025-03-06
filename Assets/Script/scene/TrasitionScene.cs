using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrasitionScene : MonoBehaviour
{
    public static TrasitionScene Instance;

    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1.0f;

   

    private void Start()
    {
        StartCoroutine(FadeIn());
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    private IEnumerator FadeIn()
    {
        fadeCanvasGroup.alpha = 1; 
        while (fadeCanvasGroup.alpha > 0)
        {
            fadeCanvasGroup.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        fadeCanvasGroup.alpha = 0;
        while (fadeCanvasGroup.alpha < 1)
        {
            fadeCanvasGroup.alpha += Time.deltaTime / fadeDuration;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(0.5f); 

        
        fadeCanvasGroup = FindObjectOfType<CanvasGroup>();
        if (fadeCanvasGroup == null)
        {
            Debug.LogError("fadeCanvasGroup not found in new scene!");
            yield break;
        }

        StartCoroutine(FadeIn());
    }
}
