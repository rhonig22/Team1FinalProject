using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _menuPopup;
    private float _timeRemaining = 16f;

    private void Update()
    {
        if (NoteManager.Instance.IsBeatStarted)
        {
            if (_timeRemaining <= 0)
            {
                _menuPopup.SetActive(true);
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
        GameManager.Instance.LoadDemo();
    }
}
