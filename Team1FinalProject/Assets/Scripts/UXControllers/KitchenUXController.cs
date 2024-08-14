using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenUXController : MonoBehaviour
{
    private void Start()
    {
        NoteManager.Instance.StartBeats();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenuClicked();
        }
    }

    public void MainMenuClicked()
    {
        NoteManager.Instance.StopBeats();
        GameManager.Instance.LoadTitleScreen();
    }
}
