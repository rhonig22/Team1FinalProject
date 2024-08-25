using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonUXController : MonoBehaviour
{
    public void MainMenuClicked()
    {
        NoteManager.Instance.StopBeats();
        GameManager.Instance.LoadTitleScreen();
    }
    public void BackClicked()
    {
        NoteManager.Instance.StopBeats();
        GameManager.Instance.GoBack();
    }
    public void SettingsClicked()
    {
        GameManager.Instance.LoadSettings();
    }
}
