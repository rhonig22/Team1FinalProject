using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeCompletedUXController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recipeText;
    [SerializeField] private StarController _starController;

    private void OnEnable()
    {
        var recipeName = RecipeManager.Instance.GetRecipeName();
        _recipeText.text = recipeName;
        var recipeEntry = SaveDataManager.Instance.GetRecipeEntry(recipeName);
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
        }
    }
}
