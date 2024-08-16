using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroStoryUXController : MonoBehaviour
{
    [SerializeField] private Conversation introConvo;
    private readonly string _firstTimeKey = "FirstTimeConvo";
    private float _dialogueStartTime = .5f;

    private void Start()
    {
        if (!SaveDataManager.Instance.IsUnlocked(_firstTimeKey))
            StartCoroutine(BeginDialogue());
        else
            GameManager.Instance.LoadRecipeBook();
    }

    public void MainMenuClicked()
    {
        GameManager.Instance.LoadTitleScreen();
    }

    private IEnumerator BeginDialogue()
    {
        yield return new WaitForSeconds(_dialogueStartTime);
        DialogueManager.Instance.DialogueFinished.AddListener(() => {
            SaveDataManager.Instance.UnlockedSomething(_firstTimeKey);
            GameManager.Instance.LoadRecipeBook(); 
        });
        DialogueManager.Instance.StartConversation(introConvo);
    }
}
