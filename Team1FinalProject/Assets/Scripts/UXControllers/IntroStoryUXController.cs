using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroStoryUXController : MonoBehaviour
{
    [SerializeField] private Conversation introConvo;
    private float _dialogueStartTime = .5f;

    private void Start()
    {
        StartCoroutine(BeginDialogue());
    }

    public void MainMenuClicked()
    {
        GameManager.Instance.LoadTitleScreen();
    }

    private IEnumerator BeginDialogue()
    {
        yield return new WaitForSeconds(_dialogueStartTime);
        DialogueManager.Instance.DialogueFinished.AddListener(() => { GameManager.Instance.LoadRecipeBook(); });
        DialogueManager.Instance.StartConversation(introConvo);
    }
}
