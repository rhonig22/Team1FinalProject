using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance;
    public bool RecipeCompleted { get; private set; } = false;
    private ScriptableRecipe _currentRecipe;
    private int _currentStepIndex;
    private int _currentIngredientSpriteIndex = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetRecipe(ScriptableRecipe recipe)
    {
        _currentRecipe = recipe;
        _currentStepIndex = 0;
        RecipeCompleted = false;
    }

    public int GetMaxScore()
    {
        return _currentRecipe.GetMaxScore();
    }

    public RecipeStep GetNextStep()
    {
        return _currentRecipe.GetStep(_currentStepIndex);
    }

    public Sprite GetCurrentIngredientSprite()
    {
        var step = _currentRecipe.GetStep(_currentStepIndex);
        return step.Ingredient.getSprite(_currentIngredientSpriteIndex);
    }

    public void IncrementIngredientSprite()
    {
        _currentIngredientSpriteIndex++;
    }

    public string GetRecipeName()
    {
        return _currentRecipe.GetName();
    }

    public int GetBPM()
    {
        return _currentRecipe.GetBPM();
    }

    public AudioClip GetBackingTrack()
    {
        return _currentRecipe.GetBackingTrack();
    }

    public void IncrementStep() { 
        _currentStepIndex++;
        _currentIngredientSpriteIndex = 0;
        if (_currentStepIndex >= _currentRecipe.GetStepCount())
            RecipeCompleted = true;
    }

}
