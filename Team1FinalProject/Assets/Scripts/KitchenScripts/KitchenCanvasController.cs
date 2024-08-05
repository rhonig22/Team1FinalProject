using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject _kitchenDimmer;
    [SerializeField] private GameObject _recipeCompletionPanel;

    public static bool IsRhythmSection = false;

    private void Update()
    {
        IsRhythmSection = LaneScroller.Instance.HasUpcomingNotes();
        _kitchenDimmer.SetActive(LaneScroller.Instance.HasUpcomingNotes());

        _recipeCompletionPanel.SetActive(RecipeManager.Instance.RecipeCompleted);
    }

    public void RecipeBookClicked()
    {
        GameManager.Instance.LoadRecipeBook();
    }
}
