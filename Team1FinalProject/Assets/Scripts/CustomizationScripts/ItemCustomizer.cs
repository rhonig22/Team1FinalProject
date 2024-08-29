using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ItemCustomizer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private Button _button;
    private ScriptableCustomization _currentCustomization;
    private List<ScriptableCustomization> _customizationList;
    public UnityEvent NewCustomizationSet = new UnityEvent();

    private void Start()
    {
        _currentCustomization = CustomizationManager.Instance.GetCurrentItem(_itemType);
        _image.sprite = _currentCustomization.GetSprite();
        _customizationList = CustomizationManager.Instance.GetUnlockedItems(_itemType);
        if (_customizationList.Count < 2)
            _button.enabled = false;
    }

    public void CustomizerClicked()
    {
        var index = _customizationList.IndexOf(_currentCustomization);
        var nextCustomization = _customizationList[(index + 1) % _customizationList.Count];
        CustomizationManager.Instance.SetCurrentCustomization(_itemType, nextCustomization.GetAesthetic());
        _image.sprite = nextCustomization.GetSprite();
        _currentCustomization = nextCustomization;
        NewCustomizationSet.Invoke();
    }
}
