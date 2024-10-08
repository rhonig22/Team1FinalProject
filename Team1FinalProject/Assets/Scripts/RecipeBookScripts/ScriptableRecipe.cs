
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute (fileName = "New Recipe", menuName = "Scriptables/ New Recipe")]
public class ScriptableRecipe : ScriptableObject
{
    [SerializeField] private string _recipeName;
    [SerializeField] private string _successMessage;
    [SerializeField] private string _motivationMessage;
    [SerializeField] private float _threeStarPercent;
    [SerializeField] private int _unlockRequirement;
    [SerializeField] private RecipeStep[] _recipeSteps;
    [SerializeField] private Sprite _recipeSprite;
    [SerializeField] private Sprite _recipeVictorySprite;
    [SerializeField] private AudioClip _backingTrack;
    [SerializeField] private int _bpm;
    [SerializeField] private bool _doubleTime;
    [SerializeField] private bool _isPlaceholder;
    private readonly int _defaultMax = 1000;

    public string GetName()
    {
        return _recipeName;
    }

    public string GetSuccessMessage()
    {
        return _successMessage;
    }

    public string GetMotivationMessage()
    {
        return _motivationMessage;
    }

    public Sprite getVictorySprite()
    {
        return _recipeVictorySprite;
    }
    public Sprite getSprite()
    {
        return _recipeSprite;
    }

    public RecipeStep GetStep(int index)
    {
        if (index < _recipeSteps.Length)
            return _recipeSteps[index];

        return null;
    }

    public int GetStepCount()
    {
        return _recipeSteps.Length;
    }

    public int GetMaxScore()
    {
        var totalScore = 0;
        foreach (var step in _recipeSteps)
        {
            totalScore += step.Ingredient.GetMaxIngredientScore();
        }

        if (totalScore > 0)
            return Mathf.FloorToInt(totalScore*_threeStarPercent);

        return _defaultMax;
    }

    public int GetUnlockRequirement()
    {
        return _unlockRequirement;
    }

    public int GetBPM()
    {
        return _bpm;
    }

    public bool IsDoubleTime()
    {
        return _doubleTime;
    }

    public bool IsPlaceholder()
    {
        return _isPlaceholder;
    }

    public AudioClip GetBackingTrack()
    {
        return _backingTrack;
    }
}

[System.Serializable]
public class RecipeStep
{
    public Station Station;
    public ScriptableIngredient Ingredient;
}

public enum Station
{
    Prep,
    Stovetop,
    Fridge,
    FlatTop
}