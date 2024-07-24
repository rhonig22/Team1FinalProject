using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUXController : MonoBehaviour
{
    public void PlayDemoClicked()
    {
        GameManager.Instance.LoadDemo();
    }

    public void PlayKitchenClicked()
    {
        GameManager.Instance.LoadKitchen();
    }

    public void SettingsClicked()
    {
        GameManager.Instance.LoadSettings();
    }
}
