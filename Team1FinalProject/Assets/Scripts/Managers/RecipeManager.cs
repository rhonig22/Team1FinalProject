using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    [SerializeField] private Conversation _tutorialConversation2;
    [SerializeField] Conversation _improvedMessage;
    [SerializeField] Conversation _notAsGoodMessage;
    [SerializeField] Conversation _finalConversation;
    public static RecipeManager Instance;
    public bool RecipeCompleted { get; private set; } = false;
    public Conversation RecipeConversation { get; private set; } = null;
    private readonly int _successMessageThreshold = 2;
    private ScriptableRecipe _currentRecipe;
    private int _currentStepIndex;
    private int _currentIngredientSpriteIndex = 0;
    private readonly string _secondTutorialKey = "SecondTutorial";

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
        RecipeConversation = null;
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

    public string GetRecipeMessage(int stars)
    {
        if (stars >= _successMessageThreshold)
            return _currentRecipe.GetSuccessMessage();

        return _currentRecipe.GetMotivationMessage();
    }

    public Sprite GetRecipeVictorySprite()
    {
        return _currentRecipe.getVictorySprite();
    }
    public bool IsCurrentIngredientAnimated()
    {
        if (_currentIngredientSpriteIndex > 0)
        {
            var step = _currentRecipe.GetStep(_currentStepIndex);
            return step.Ingredient.IsAnimatedIngredient();
        }

        return false;
    }

    public string GetAnimationTrigger()
    {
        var step = _currentRecipe.GetStep(_currentStepIndex);
        return step.Ingredient.GetAnimationTrigger();
    }

    public int GetBPM()
    {
        return _currentRecipe.GetBPM();
    }

    public bool IsDoubleTime()
    {
        return _currentRecipe.IsDoubleTime();
    }

    public AudioClip GetBackingTrack()
    {
        return _currentRecipe.GetBackingTrack();
    }
    public void ClearMessage()
    {
        RecipeConversation = null;
    }

    public void SetNotAsGoodMessage()
    {
        RecipeConversation = _notAsGoodMessage;
    }

    public void SetImprovedMessage()
    {
        RecipeConversation = _improvedMessage;
    }

    public void SetFinalMessage()
    {
        RecipeConversation = _finalConversation;
    }

    public void IncrementStep() { 
        _currentStepIndex++;
        _currentIngredientSpriteIndex = 0;
        if (_currentStepIndex >= _currentRecipe.GetStepCount())
            RecipeCompleted = true;

        if (!SaveDataManager.Instance.IsUnlocked(_secondTutorialKey))
            Tutorial2Dialogue();
    }

    private void Tutorial2Dialogue()
    {
        DialogueManager.Instance.DialogueFinished.AddListener(() => {
            SaveDataManager.Instance.UnlockedSomething(_secondTutorialKey);
        });
        DialogueManager.Instance.StartConversation(_tutorialConversation2);
    }
}
