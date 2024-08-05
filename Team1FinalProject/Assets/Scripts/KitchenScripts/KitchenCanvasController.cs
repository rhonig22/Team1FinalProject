using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KitchenCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _kitchenDimmer;
    [SerializeField] private GameObject _recipeCompletionPanel;
    [SerializeField] private TextMeshProUGUI _recipeText;
    [SerializeField] private Image _ingredientImage;

    public static bool IsRhythmSection = false;

    private void Update()
    {
        var hasNotes = LaneScroller.Instance.HasUpcomingNotes();
        IsRhythmSection = hasNotes;
        _kitchenDimmer.SetActive(hasNotes);
        if (hasNotes)
            _ingredientImage.sprite = RecipeManager.Instance.GetCurrentIngredientSprite();

        var wasActive = _recipeCompletionPanel.activeInHierarchy;
        _recipeCompletionPanel.SetActive(RecipeManager.Instance.RecipeCompleted);
        if (!wasActive && RecipeManager.Instance.RecipeCompleted)
        {
            _recipeText.text = RecipeManager.Instance.GetRecipeName();
        }
    }

    public void RecipeBookClicked()
    {
        GameManager.Instance.LoadRecipeBook();
    }
}
