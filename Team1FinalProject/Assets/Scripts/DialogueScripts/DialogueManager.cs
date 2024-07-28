using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using JetBrains.Annotations;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI speakerName, dialogue, navButtonText;
    public Image speakerSprite;

    private int currentIndex;
    private Conversation currentConvo;
    private static DialogueManager instance;
    private Coroutine typing;

    private Animator anim;
    private void Awake()
    {
        if (instance == null)
        {
            //make the dialogue manager this one
            instance = this;
            anim = GetComponent<Animator>();
        }
        else
        {
            //only one at a time is allowed
            Destroy(gameObject);
        }
    }

    public static void StartConversation(Conversation convo)
    {
        instance.anim.SetBool("isOpen", true);
        instance.currentIndex = 0;
        instance.currentConvo = convo;
        instance.speakerName.text = "";
        instance.dialogue.text = "";
        instance.navButtonText.text = ">";

        instance.ReadNext();

    }
    public void ReadNext()
    {
        if (currentIndex >= currentConvo.GetLength())
        {
            //Debug.Log("currentIndex: " + currentIndex + " and currentConvo: " + currentConvo.GetLength());
            navButtonText.text = "X";
        }
        if (currentIndex > currentConvo.GetLength())
        {
            instance.anim.SetBool("isOpen", false);
            return;
        }
        else
        {
            speakerName.text = currentConvo.getLineByIndex(currentIndex).speaker.GetName();

            if (typing == null)
            {
                typing = instance.StartCoroutine(TypeText(currentConvo.getLineByIndex(currentIndex).dialogue));
            }
            else
            {
                instance.StopCoroutine(typing);
                typing = null;
                typing = instance.StartCoroutine(TypeText(currentConvo.getLineByIndex(currentIndex).dialogue));

            }
            //dialogue.text = currentConvo.getLineByIndex(currentIndex).dialogue;
            speakerSprite.sprite = currentConvo.getLineByIndex(currentIndex).speaker.getSprite();
            currentIndex++;
        }
       
    }
    private IEnumerator TypeText(string text)
    {
        dialogue.text = "";
        bool complete = false;
        int index = 0;

        while (!complete)
        {
            dialogue.text += text[index];
            yield return new WaitForSeconds(0.05f);

            if(index == text.Length)
            {
                complete = true;
            }
            index++;
        }
        typing = null;
    }

}
