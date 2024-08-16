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
        if (_currentRecipe != null)
            return _currentRecipe.GetMaxScore();

        return 0;
    }

    public float GetPercentCompleted()
    {
        if (_currentRecipe != null)
        {
            return _currentStepIndex / _currentRecipe.GetStepCount();
        }

        return 0;
    }

    public RecipeStep GetNextStep()
    {
        if (_currentRecipe != null)
            return _currentRecipe.GetStep(_currentStepIndex);

        return null;
    }

    public Station GetNextStation()
    {
        var step = GetNextStep();
        if (step != null)
            return GetNextStep().Station;

        return Station.Prep;
    }

    public Sprite GetCurrentIngredientSprite()
    {
        var step = _currentRecipe.GetStep(_currentStepIndex);
        return step.Ingredient.getSprite(_currentIngredientSpriteIndex);
    }
    public Sprite GetCurrentBackgroundPrepSprite()
    {
        var step = _currentRecipe.GetStep(_currentStepIndex);
        return step.Ingredient.getBGPrepSprite(_currentIngredientSpriteIndex);
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
