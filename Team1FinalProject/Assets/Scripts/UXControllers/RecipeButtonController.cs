using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using mixpanel;

public class RecipeButtonController : MonoBehaviour
{
    [SerializeField] private ScriptableRecipe _recipe;
    [SerializeField] private TextMeshProUGUI _recipeNameText;
    [SerializeField] private Image _recipeImage;
    [SerializeField] private GameObject _unlockedArea;
    [SerializeField] private GameObject _lockedArea;
    [SerializeField] private TextMeshProUGUI _unlockRequirement;
    [SerializeField] private Button _button;


    public void RecipeClicked()
    {
        var props = new Value();
        props["recipeName"] = _recipe.GetName();
        MixpanelLogger.Instance.LogEvent("Recipe Started", props);
        RecipeManager.Instance.SetRecipe(_recipe);
        GameManager.Instance.LoadKitchen();
    }

    public void SetRecipe(ScriptableRecipe recipe)
    {
        _recipe = recipe;
        _recipeNameText.text = recipe.GetName();
        _recipeImage.sprite = recipe.getSprite();
        bool unlocked = recipe.GetUnlockRequirement() <= SaveDataManager.Instance.GetStarCount();
        _unlockedArea.SetActive(unlocked);
        _lockedArea.SetActive(!unlocked);
        _unlockRequirement.text = "" + recipe.GetUnlockRequirement();
        if (!unlocked)
            _button.enabled = false;
    }
}
