
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenuAttribute (fileName = "New Customization", menuName = "Scriptables/ New Customization")]
public class ScriptableCustomization : ScriptableObject
{
    [SerializeField] private string _customizationName;
    [SerializeField] private Sprite _customizationSprite;
    [SerializeField] private Aesthetic _aesthetic;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private UnlockRequirement _requirement;
    [SerializeField] private bool _isBase;
    private bool _unlocked { get; set; } = false;

    public bool GetInitialUnlock()
    {
        if (SaveDataManager.Instance.IsUnlocked(_customizationName))
            _unlocked = true;
        else
            _unlocked = false;

        return _unlocked; 
    }

    public bool CheckUnlockRequirement()
    {
        if (_unlocked || _isBase)
            return false;

        bool shouldUnlock = true;
        foreach (var recipe in _requirement.Recipes)
        {
            var entry = SaveDataManager.Instance.GetRecipeEntry(recipe);
            if (entry == null) {
                shouldUnlock = false;
                continue; 
            }

            int currentVal = 0;
            switch (_requirement.RequirementType)
            {
                case RequirementType.Stars:
                    currentVal = entry.Stars;
                    break;

                case RequirementType.Score:
                    currentVal = entry.HighScore;
                    break;
            }

            shouldUnlock &= (currentVal >= _requirement.MinValue);
        }

        if (shouldUnlock)
        {
            SaveDataManager.Instance.UnlockedSomething(_customizationName);
            _unlocked = true;
        }

        return shouldUnlock;
    }

    public bool IsUnlocked()
    {
        return _unlocked || _isBase || GameManager.IsUnlockedMode;
    }

    public string GetName()
    {
        return _customizationName;
    }

    public Sprite GetSprite()
    {
        return _customizationSprite;
    }

    public Aesthetic GetAesthetic()
    {
        return _aesthetic;
    }

    public ItemType GetItemType() { return _itemType; }
}

public enum Aesthetic
{
    Modest,
    Vaporwave,
    Explorer,
    FineDining
}

public enum ItemType
{
    Prep,
    Fridge,
    Stovetop,
    Flattop,
    Wall,
    Floor,
    Other
}