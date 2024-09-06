using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HubUXController : MonoBehaviour
{
    [SerializeField] GameObject _customizerPanel;
    [SerializeField] GameObject _customizerDoneButton;
    [SerializeField] GameObject _customizeKitchenButton;
    [SerializeField] private AudioClip _buttonClick;
    public void RecipeBookClicked()
    {
        SoundManager.Instance.PlaySound(_buttonClick, transform.position);
        GameManager.Instance.LoadRecipeBook();
    }

    public void LeaderboardClicked()
    {
        SoundManager.Instance.PlaySound(_buttonClick, transform.position);
        GameManager.Instance.LoadLeaderboardScene();
    }

    public void CustomizeKitchenClicked()
    {
        _customizerPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_customizerDoneButton);
        SoundManager.Instance.PlaySound(_buttonClick, transform.position);
    }

    public void DoneCustomizingClicked()
    {
        _customizerPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(_customizeKitchenButton);
        SoundManager.Instance.PlaySound(_buttonClick, transform.position);
    }
}
