using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUXController : MonoBehaviour
{
    [SerializeField] private AudioClip _buttonClick;

    public void PlayStoryClicked()
    {
        SoundManager.Instance.PlaySound(_buttonClick, transform.position);
        GameManager.Instance.LoadIntro();
    }

    public void SettingsClicked()
    {
        SoundManager.Instance.PlaySound(_buttonClick, transform.position);
        GameManager.Instance.LoadSettings();
    }

    public void CreditsClicked()
    {
        SoundManager.Instance.PlaySound(_buttonClick, transform.position);
        GameManager.Instance.LoadCredits();
    }
}
