using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubUXController : MonoBehaviour
{
    [SerializeField] GameObject _customizerPanel;
    public void RecipeBookClicked()
    {
        GameManager.Instance.LoadRecipeBook();
    }

    public void CustomizeKitchenClicked()
    {
        _customizerPanel.SetActive(true);
    }

    public void DoneCustomizingClicked()
    {
        _customizerPanel.SetActive(false);
    }
}
