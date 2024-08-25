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
        GetCurrentCustomizations();
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

    public ScriptableCustomization GetCurrentItem(ItemType itemType)
    {
        return _allCustomizations.Find((ScriptableCustomization x) => x.GetAesthetic() == _currentItemTypes[itemType] && x.GetItemType() == itemType);
    }

}
