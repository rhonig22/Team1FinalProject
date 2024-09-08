using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class RecipeUXController : MonoBehaviour
{
    [SerializeField] GameObject _unlockScreen;
    [SerializeField] TextMeshProUGUI _unlockText;
    [SerializeField] TextMeshProUGUI _starCount;
    [SerializeField] Image _unlockImage;
    [SerializeField] RecipeBookController _recipeBook;
    [SerializeField] Conversation _needToRetryMessage;
    [SerializeField] Conversation _redecorateMessage;
    private readonly float _dialogueStartTime = .5f;
    private readonly string _redecorateMessageKey = "firstDecoration";
    public static bool DontSelectRecipe = false;
    private List<ScriptableCustomization> _unlocks;
    private bool _isUnlockOpen = false;

    private void Start()
    {
        _starCount.text = "" + SaveDataManager.Instance.GetStarCount();
        _unlocks = CustomizationManager.Instance.CheckUnlocks();
        if (_unlocks.Count > 0)
        {
            string newText = string.Empty;
            foreach (var unlock in _unlocks)
            {
                newText += unlock.GetName() + "\r\n";
            }

            DontSelectRecipe = true;
            var unlockSprite = _unlocks[0].GetSprite();
            _unlockImage.sprite = unlockSprite;
            _unlockImage.rectTransform.sizeDelta = new Vector2(unlockSprite.rect.width, unlockSprite.rect.height);
            _unlockText.text = newText;
            _unlockScreen.SetActive(true);
            _isUnlockOpen = true;
        }
        else
        {
            DontSelectRecipe = false;
        }

        StartCoroutine(CheckForDialogue());
    }

    public void MainMenuClicked()
    {
        GameManager.Instance.LoadTitleScreen();
    }

    public void CloseUnlockScreen()
    {
        _unlockScreen.SetActive(false);
        _isUnlockOpen = false;
        _recipeBook.SelectCurrentPageButton();
    }

    public void CloseAndSwapUnlockScreen()
    {
        if (_unlocks.Count > 0)
        {
            CustomizationManager.Instance.SetCurrentCustomization(_unlocks[0].GetItemType(), _unlocks[0].GetAesthetic());
            _unlocks.Clear();
        }

        _unlockScreen.SetActive(false);
        _isUnlockOpen = false;
        _recipeBook.SelectCurrentPageButton();

        if (!SaveDataManager.Instance.IsUnlocked(_redecorateMessageKey))
            ShowDialogue(_redecorateMessage);
    }

    private IEnumerator CheckForDialogue()
    {
        yield return new WaitForSeconds(_dialogueStartTime);
        var conversation = RecipeManager.Instance.RecipeConversation;
        if (conversation == null && _recipeBook.NeedToRetry)
        {
            conversation = _needToRetryMessage;
            RecipeBookController.ClearRetryMessage();
        }

        if (conversation != null)
        {
            ShowDialogue(conversation);
        }
    }

    private void ShowDialogue(Conversation conversation)
    {
        DialogueManager.Instance.DialogueFinished.AddListener(() => {
            if (_isUnlockOpen)
                _unlockScreen.GetComponentInChildren<UnlockedUXController>().SelectDoneButton();
            else
                _recipeBook.SelectCurrentPageButton();

            if (conversation == _redecorateMessage)
                SaveDataManager.Instance.UnlockedSomething(_redecorateMessageKey);
        });
        DialogueManager.Instance.StartConversation(conversation);
    }
}
