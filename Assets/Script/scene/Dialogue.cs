using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueCanvas;
    [SerializeField]
    private TMP_Text speakerText;
    [SerializeField]
    private TMP_Text dialogueText;
    [SerializeField]
    private Image portraitImage;

    [SerializeField]
    private string[] speaker;
    [SerializeField]
    [TextArea]
    private string[] dialogueWords;
    [SerializeField]
    private Sprite[] portrait;
    private int step;

    private bool dialogueActivated;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) && dialogueActivated == true)
        {
            if (step >= speaker.Length) { 
             dialogueCanvas.SetActive(false);
                step = 0;
            }
            else
            {
                dialogueCanvas.SetActive(true);
                speakerText.text = speaker[step];
                dialogueText.text = dialogueWords[step];
                portraitImage.sprite = portrait[step];
                step += 1;
            }
            
        
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            dialogueActivated = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        dialogueActivated = false;
        dialogueCanvas.SetActive(false);
    }
}
