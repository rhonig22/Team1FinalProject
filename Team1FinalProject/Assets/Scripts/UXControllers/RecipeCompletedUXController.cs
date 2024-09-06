using mixpanel;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeCompletedUXController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recipeText;
    [SerializeField] private StarController _starController;
    [SerializeField] private CanvasRenderer _victoryFoodRenderer;
    [SerializeField] private Button _completedButton;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_completedButton.gameObject);
        var recipeName = RecipeManager.Instance.GetRecipeName();
       
        _recipeText.text = recipeName;
      
        var recipeEntry = SaveDataManager.Instance.GetRecipeEntry(recipeName);
        //not yet working dynamically?
        _victoryFoodRenderer.GetComponent<Image>().sprite = RecipeManager.Instance.GetRecipeVictorySprite();

        if (recipeEntry == null )
        {
            recipeEntry = new RecipeEntry();
            recipeEntry.Name = recipeName;
            recipeEntry.HighScore = 0;
        }

        if (NoteManager.Instance.Score > recipeEntry.HighScore)
        {
            _starController.SetMaxScore(RecipeManager.Instance.GetMaxScore());
            _starController.SetScore(NoteManager.Instance.Score);
            recipeEntry.Stars = _starController.GetCurrentStarCount();
            recipeEntry.HighScore = NoteManager.Instance.Score;
            SaveDataManager.Instance.SetRecipeEntryData(recipeEntry);
            LeaderboardManager.Instance.SubmitLootLockerScore(SaveDataManager.Instance.GetStarCount());
        }

        var props = new Value();
        props["recipeName"] = recipeName;
        props["score"] = NoteManager.Instance.Score;
        MixpanelLogger.Instance.LogEvent("Recipe Completed", props);
    }
}
