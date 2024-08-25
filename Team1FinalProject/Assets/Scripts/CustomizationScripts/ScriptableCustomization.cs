
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