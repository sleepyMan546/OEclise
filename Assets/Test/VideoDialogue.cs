using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoDialogue : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer; 
    [SerializeField] private VideoClip[] videoClips;
    [SerializeField] private int sceneIndex = 1; 
    [SerializeField] private Button nextButton; 
    [SerializeField] private Button skipButton; 
    [SerializeField] private Image loadingScreen; 

    private int currentClipIndex = 0; 
    private bool isVideoReady = false; 
    private bool isVideoFinished = false; 

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
            if (videoPlayer == null)
            {
                Debug.LogError("VideoPlayer component not found!");
                return;
            }
        }

        if (videoClips.Length == 0)
        {
            Debug.LogError("No video clips assigned!");
            return;
        }

     
        if (nextButton == null || skipButton == null)
        {
            Debug.LogWarning("NextButton or SkipButton not assigned in Inspector!");
        }

        if (nextButton != null)
        {
            nextButton.interactable = false;
        }

       
        if (loadingScreen != null)
        {
            loadingScreen.gameObject.SetActive(false);
        }

        SetupVideoPlayer();
        PlayCurrentClip();
    }

    void Update()
    {
     
        if (nextButton != null)
        {
            nextButton.interactable = isVideoFinished; 
        }
    }

    void SetupVideoPlayer()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.isLooping = false;
    }

    void PlayCurrentClip()
    {
        isVideoReady = false;
        isVideoFinished = false;
        videoPlayer.clip = videoClips[currentClipIndex];
        videoPlayer.Prepare();
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        isVideoReady = true;
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        isVideoFinished = true;
    }

    
    public void NextClip()
    {
        if (isVideoFinished)
        {
            currentClipIndex++;

            if (currentClipIndex < videoClips.Length)
            {
                PlayCurrentClip();
            }
            else
            {
                StartCoroutine(LoadSceneWithLoadingScreen());
            }
        }
    }

   
    public void SkipCutscene()
    {
        StartCoroutine(SkipWithLoadingScreen());
    }

   
    private IEnumerator LoadSceneWithLoadingScreen()
    {
       
        if (loadingScreen != null)
        {
            loadingScreen.gameObject.SetActive(true);
           
            Color screenColor = loadingScreen.color;
            screenColor.a = 1f;
            loadingScreen.color = screenColor;
        }

      
        videoPlayer.Stop();

        yield return new WaitForSeconds(0.1f);

      
        SceneManager.LoadScene(sceneIndex);
    }

    
    private IEnumerator SkipWithLoadingScreen()
    {
       
        if (loadingScreen != null)
        {
            loadingScreen.gameObject.SetActive(true);
            Color screenColor = loadingScreen.color;
            screenColor.a = 1f;
            loadingScreen.color = screenColor;
        }

      
        videoPlayer.Stop();

        
        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene(sceneIndex);
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
            videoPlayer.prepareCompleted -= OnVideoPrepared;
        }
    }
}
