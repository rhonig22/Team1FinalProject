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
    [SerializeField] private GameObject _comingSoon;
    [SerializeField] private TextMeshProUGUI _unlockRequirement;
    [SerializeField] private Button _button;
    [SerializeField] private AudioClip _buttonClick;
    public bool IsUnlocked { get; private set; } = false;

    public void RecipeClicked()
    {
        SoundManager.Instance.PlaySound(_buttonClick, transform.position);
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
        IsUnlocked = recipe.GetUnlockRequirement() <= SaveDataManager.Instance.GetStarCount() || GameManager.IsUnlockedMode;
        _unlockedArea.SetActive(IsUnlocked);
        _lockedArea.SetActive(!IsUnlocked);
        _unlockRequirement.text = "" + recipe.GetUnlockRequirement();
        if (!IsUnlocked || recipe.IsPlaceholder())
            _button.enabled = false;

        if (recipe.IsPlaceholder())
            _comingSoon.SetActive(true);
    }
}
