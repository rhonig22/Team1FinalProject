using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance;
    public bool RecipeCompleted { get; private set; } = false;
    private ScriptableRecipe _currentRecipe;
    private int _currentStepIndex;

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
    }

    public int GetMaxScore()
    {
        return _currentRecipe.GetMaxScore();
    }

    public RecipeStep GetNextStep()
    {
        return _currentRecipe.GetStep(_currentStepIndex);
    }

    public void IncrementStep() { 
        _currentStepIndex++;
        if (_currentStepIndex >= _currentRecipe.GetStepCount())
            RecipeCompleted = true;
    }

}
