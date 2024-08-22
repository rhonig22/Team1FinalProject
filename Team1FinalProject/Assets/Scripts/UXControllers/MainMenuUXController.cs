using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUXController : MonoBehaviour
{
    public void PlayDemoClicked()
    {
        GameManager.Instance.LoadDemo();
    }

    public void PlayDemoV2Clicked()
    {
        GameManager.Instance.LoadDemoV2();
    }

    public void PlayStoryClicked()
    {
        GameManager.Instance.LoadIntro();
    }

    public void SettingsClicked()
    {
        GameManager.Instance.LoadSettings();
    }

    public void ControlsClicked()
    {
        Debug.Log("Controls time!");
        GameManager.Instance.LoadControls();
    }
    public void CreditsClicked()
    {
        Debug.Log("Credits Time!");
        GameManager.Instance.LoadCredits();
    }
}
