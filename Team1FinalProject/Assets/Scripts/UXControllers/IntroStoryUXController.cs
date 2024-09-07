using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroStoryUXController : MonoBehaviour
{
    [SerializeField] private Conversation _grannyConvo;
    [SerializeField] private Conversation welcomeBackConvo;
    private readonly string _firstTimeKey = "FirstTimeConvo";
    private float _dialogueStartTime = .5f;

    private void Start()
    {
        if (!SaveDataManager.Instance.IsUnlocked(_firstTimeKey))
            StartCoroutine(BeginDialogue());
        else
        {
            StartCoroutine(BeginWelcomeBackDialogue());
        }
    }

    private IEnumerator BeginDialogue()
    {
        yield return new WaitForSeconds(_dialogueStartTime);
        DialogueManager.Instance.DialogueFinished.AddListener(() => {
            SaveDataManager.Instance.UnlockedSomething(_firstTimeKey);
            GameManager.Instance.LoadHubScene();
        });
        DialogueManager.Instance.StartConversation(_grannyConvo);
    }
    private IEnumerator BeginWelcomeBackDialogue()
    {
        yield return new WaitForSeconds(_dialogueStartTime);
        DialogueManager.Instance.DialogueFinished.AddListener(() => {
            GameManager.Instance.LoadHubScene();
        });
        DialogueManager.Instance.StartConversation(welcomeBackConvo);
    }
}
