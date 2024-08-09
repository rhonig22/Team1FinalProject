using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenUXController : MonoBehaviour
{
    private void Start()
    {
        NoteManager.Instance.StartBeats();
    }

    public void MainMenuClicked()
    {
        NoteManager.Instance.StopBeats();
        GameManager.Instance.LoadTitleScreen();
    }
}
