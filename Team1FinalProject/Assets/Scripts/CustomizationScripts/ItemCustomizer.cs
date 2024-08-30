using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ItemCustomizer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonText;
    private ScriptableCustomization _currentCustomization;
    private List<ScriptableCustomization> _customizationList;
    public UnityEvent NewCustomizationSet = new UnityEvent();

    private void Start()
    {
        _currentCustomization = CustomizationManager.Instance.GetCurrentItem(_itemType);
        _image.sprite = _currentCustomization.GetSprite();
        _buttonText.text = _currentCustomization.GetName();
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
        _buttonText.text = nextCustomization.GetName();
        _currentCustomization = nextCustomization;
        NewCustomizationSet.Invoke();
    }
}
