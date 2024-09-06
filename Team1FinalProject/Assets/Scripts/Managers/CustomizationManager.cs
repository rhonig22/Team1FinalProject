using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationManager : MonoBehaviour
{
    public static CustomizationManager Instance;
    [SerializeField] private List<ScriptableCustomization> _allCustomizations;
    private Dictionary<ItemType, Aesthetic> _currentItemTypes = new Dictionary<ItemType, Aesthetic>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        ResetCustomizations();
    }

    public void ResetCustomizations()
    {
        SetInitialUnlocks();
        GetCurrentCustomizations();
    }

    private void SetInitialUnlocks()
    {
        foreach (var customization in _allCustomizations)
        {
            customization.GetInitialUnlock();
        }
    }

    private void GetCurrentCustomizations()
    {
        var data = SaveDataManager.Instance.GetPlayerData();
        _currentItemTypes[ItemType.Flattop] = data.Flattop;
        _currentItemTypes[ItemType.Floor] = data.Floor;
        _currentItemTypes[ItemType.Wall] = data.Wall;
        _currentItemTypes[ItemType.Prep] = data.Prep;
        _currentItemTypes[ItemType.Fridge] = data.Fridge;
        _currentItemTypes[ItemType.Stovetop] = data.Stovetop;
    }

    public void SetCurrentCustomization(ItemType type, Aesthetic aesthetic)
    {
        _currentItemTypes[type] = aesthetic;
        var data = SaveDataManager.Instance.GetPlayerData();
        switch (type) {
            case ItemType.Flattop:
                data.Flattop = aesthetic;
                break;
            case ItemType.Floor:
                data.Floor = aesthetic;
                break;
            case ItemType.Wall:
                data.Wall = aesthetic;
                break;
            case ItemType.Prep:
                data.Prep = aesthetic;
                break;
            case ItemType.Fridge:
                data.Fridge = aesthetic;
                break;
            case ItemType.Stovetop:
                data.Stovetop = aesthetic;
                break;
        }

        SaveDataManager.Instance.SetPlayerData(data);
    }

    public List<ScriptableCustomization> CheckUnlocks()
    {
        var newUnlocks = new List<ScriptableCustomization>();
        foreach (var customization in _allCustomizations)
        {
            if (customization.CheckUnlockRequirement())
                newUnlocks.Add(customization);
        }

        return newUnlocks;
    }

    public ScriptableCustomization GetCurrentItem(ItemType itemType)
    {
        return _allCustomizations.Find((ScriptableCustomization x) => x.GetAesthetic() == _currentItemTypes[itemType] && x.GetItemType() == itemType);
    }

    public List<ScriptableCustomization> GetUnlockedItems(ItemType itemType)
    {
        return _allCustomizations.FindAll((ScriptableCustomization x) => x.IsUnlocked() && x.GetItemType() == itemType);
    }

}
