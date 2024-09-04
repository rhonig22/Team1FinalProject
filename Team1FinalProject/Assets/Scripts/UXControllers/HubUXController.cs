using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HubUXController : MonoBehaviour
{
    [SerializeField] GameObject _customizerPanel;
    [SerializeField] GameObject _customizerDoneButton;
    [SerializeField] GameObject _customizeKitchenButton;
    public void RecipeBookClicked()
    {
        GameManager.Instance.LoadRecipeBook();
    }

    public void CustomizeKitchenClicked()
    {
        _customizerPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_customizerDoneButton);
    }

    public void DoneCustomizingClicked()
    {
        _customizerPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_customizeKitchenButton);
    }
}
