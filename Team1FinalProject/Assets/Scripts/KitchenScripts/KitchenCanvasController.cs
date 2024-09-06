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
    private bool _showRecipeCompleted = false;
    private bool _canStartAnimationFlag = false;
    private float _waitToComplete = 1.5f;
    private Coroutine _completedCoroutine;
    public static bool IsRhythmSection = false;

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
}
