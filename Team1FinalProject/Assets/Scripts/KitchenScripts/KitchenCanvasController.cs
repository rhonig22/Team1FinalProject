using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KitchenCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _kitchenDimmer;
    [SerializeField] private GameObject _recipeCompletionPanel;
    [SerializeField] private Image _ingredientImage;
    [SerializeField] private Image _backgroundPrepImage;
    [SerializeField] private Animator _ingredientAnimator;
    [SerializeField] private AnimatorDone _animationDone;
    [SerializeField] private Conversation _tutorial1Keyboard;
    [SerializeField] private Conversation _tutorial1Controller;
    [SerializeField] private Conversation _shakshukaMessage;
    private bool _showRecipeCompleted = false;
    private bool _canStartAnimationFlag = false;
    private float _waitToComplete = 1.5f;
    private Coroutine _completedCoroutine;
    public static bool IsRhythmSection = false;
    private readonly string _firstTutorialKey = "FirstTutorial";
    private readonly string _shakshukaKey = "ShakeShakshuka";
    private readonly string _finalRecipeName = "Shakshuka";
    private float _dialogueStartTime = .5f;

    private void Start()
    {
        if (!SaveDataManager.Instance.IsUnlocked(_firstTutorialKey))
            StartCoroutine(Tutorial1Dialogue());
        else if (RecipeManager.Instance.GetRecipeName() == _finalRecipeName && !SaveDataManager.Instance.IsUnlocked(_shakshukaKey))
            StartCoroutine(ShakshukaDialogue());
    }

    private void Update()
    {
        var hasNotes = LaneScroller.Instance.IsIngredientRunning();
        if (hasNotes && !IsRhythmSection)
        {
            _canStartAnimationFlag = true;
            _animationDone.AnimatorDoneFlag = false;
        }

        IsRhythmSection = hasNotes;
        _kitchenDimmer.SetActive(hasNotes);
        if (hasNotes)
        {
            if (RecipeManager.Instance.IsCurrentIngredientAnimated() && !_animationDone.AnimatorDoneFlag)
            {
                if (_canStartAnimationFlag)
                {
                    _ingredientAnimator.enabled = true;
                    _ingredientAnimator.SetTrigger(RecipeManager.Instance.GetAnimationTrigger());
                    _canStartAnimationFlag = false;
                }
            }
            else
            {
                _ingredientAnimator.enabled = false;
                _ingredientImage.sprite = RecipeManager.Instance.GetCurrentIngredientSprite();
            }

            _backgroundPrepImage.sprite = RecipeManager.Instance.GetCurrentBackgroundPrepSprite();
        }

        _recipeCompletionPanel.SetActive(_showRecipeCompleted);
        if (!_showRecipeCompleted && RecipeManager.Instance.RecipeCompleted && _completedCoroutine == null)
        {
            _completedCoroutine = StartCoroutine(RecipeComplete());
        }
    }

    IEnumerator RecipeComplete()
    {
        yield return new WaitForSeconds(_waitToComplete);
        _showRecipeCompleted = true;
    }

    public void RecipeBookClicked()
    {
        NoteManager.Instance.StopBeats();
        GameManager.Instance.PopBack();
        GameManager.Instance.LoadRecipeBook(false);
    }

    private IEnumerator Tutorial1Dialogue()
    {
        yield return new WaitForSeconds(_dialogueStartTime);
        DialogueManager.Instance.DialogueFinished.AddListener(() => {
            SaveDataManager.Instance.UnlockedSomething(_firstTutorialKey);
        });
        DialogueManager.Instance.StartConversation(GameManager.IsController ? _tutorial1Controller : _tutorial1Keyboard);
    }

    private IEnumerator ShakshukaDialogue()
    {
        yield return new WaitForSeconds(_dialogueStartTime);
        DialogueManager.Instance.DialogueFinished.AddListener(() => {
            SaveDataManager.Instance.UnlockedSomething(_shakshukaKey);
        });
        DialogueManager.Instance.StartConversation(_shakshukaMessage);
    }
}
