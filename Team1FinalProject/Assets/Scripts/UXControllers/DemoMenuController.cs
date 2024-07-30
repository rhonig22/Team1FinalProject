using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _menuPopup;
    private float _timeRemaining = 30f;

    private void Update()
    {
        if (NoteManager.Instance.IsBeatStarted)
        {
            if (_timeRemaining <= 0)
            {
                _menuPopup.SetActive(true);
                NoteManager.Instance.StopBeats();
            }
            else
            {
                _timeRemaining -= Time.deltaTime;
            }
        }
    }

    public void MainMenuClicked()
    {
        NoteManager.Instance.StopBeats();
        GameManager.Instance.LoadTitleScreen();
    }

    public void RestartClicked()
    {
        NoteManager.Instance.StopBeats();
        GameManager.Instance.LoadDemoV2();
    }
}
