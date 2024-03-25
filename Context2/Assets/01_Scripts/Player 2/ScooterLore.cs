using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScooterLore : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();


        // REMOVE LINE WHEN CALLED MANUALY THROUGH TITLE SCREEN
        StartDialogue();
    }

    void Update()
    {
        
    }

    public void StartDialogue()
    {
        anim.SetTrigger("next");
        StartCoroutine(DialogueTimer());
    }

    IEnumerator DialogueTimer()
    {
        yield return new WaitForSeconds(4);
        anim.SetTrigger("next");
        StartCoroutine(DialogueTimer());
    }
}
