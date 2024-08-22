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

    public static bool IsRhythmSection = false;

    private void Update()
    {
        var hasNotes = LaneScroller.Instance.HasUpcomingNotes();
        IsRhythmSection = hasNotes;
        _kitchenDimmer.SetActive(hasNotes);
        if (hasNotes)
        {
            if (RecipeManager.Instance.IsCurrentIngredientAnimated())
            {
                _ingredientAnimator.SetTrigger(RecipeManager.Instance.GetAnimationTrigger());
            }
            else
            {
                _ingredientImage.sprite = RecipeManager.Instance.GetCurrentIngredientSprite();
            }

            _backgroundPrepImage.sprite = RecipeManager.Instance.GetCurrentBackgroundPrepSprite();
        }
        _recipeCompletionPanel.SetActive(RecipeManager.Instance.RecipeCompleted);
    }

    public void RecipeBookClicked()
    {
        NoteManager.Instance.StopBeats();
        GameManager.Instance.LoadRecipeBook();
    }
}
