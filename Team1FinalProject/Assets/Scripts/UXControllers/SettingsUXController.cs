using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsUXController : MonoBehaviour
{
    public void MainMenuClicked()
    {
        GameManager.Instance.LoadTitleScreen();
    }
}
