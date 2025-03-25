using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScale : MonoBehaviour
{
    private Animator anim;
    private Hp hp;

    private void Start()
    {
        anim = GetComponent<Animator>();
        hp = FindObjectOfType<Hp>();
        if (hp != null)
        {
            hp.onTakeDamage.AddListener(TextScalez);
        }
    }

    public void TextScalez()
    {
        anim.SetTrigger("Scale"); 
    }
}
