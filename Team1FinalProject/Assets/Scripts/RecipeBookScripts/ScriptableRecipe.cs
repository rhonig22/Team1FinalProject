
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute (fileName = "New Recipe", menuName = "Recipes/ New Recipe")]
public class ScriptableRecipe : ScriptableObject
{
    [SerializeField] private string _recipeName;
    [SerializeField] private int _threeStarScore;
    [SerializeField] private RecipeStep[] _recipeSteps;
    [SerializeField] private Sprite _recipeSprite;
    [SerializeField] private AudioClip _backingTrack;
    [SerializeField] private int _bpm;

    public string GetName()
    {
        return _recipeName;
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
        return _threeStarScore;
    }

    public int GetBPM()
    {
        return _bpm;
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