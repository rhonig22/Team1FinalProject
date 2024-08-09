using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeButtonController : MonoBehaviour
{
    [SerializeField] private ScriptableRecipe _recipe;
    [SerializeField] private TextMeshProUGUI _recipeNameText;
    [SerializeField] private Image _recipeImage;

    public void RecipeClicked()
    {
        RecipeManager.Instance.SetRecipe(_recipe);
        GameManager.Instance.LoadKitchen();
    }

    public void SetRecipe(ScriptableRecipe recipe)
    {
        _recipe = recipe;
        _recipeNameText.text = recipe.GetName();
        _recipeImage.sprite = recipe.getSprite();
    }
}
