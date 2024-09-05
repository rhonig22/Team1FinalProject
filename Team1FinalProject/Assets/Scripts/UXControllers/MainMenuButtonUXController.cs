using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButtonUXController : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private GameObject _noteCursor;

    public void OnSelect(BaseEventData eventData)
    {
        if (_noteCursor != null)
            _noteCursor.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (_noteCursor != null)
            _noteCursor.SetActive(false);
    }

    public void MainMenuClicked()
    {
        SoundManager.Instance.PlaySound(_buttonClick, transform.position);
        NoteManager.Instance.StopBeats();
        GameManager.Instance.LoadTitleScreen();
    }
    public void BackClicked()
    {
        SoundManager.Instance.PlaySound(_buttonClick, transform.position);
        NoteManager.Instance.StopBeats();
        GameManager.Instance.GoBack();
    }
    public void SettingsClicked()
    {
        SoundManager.Instance.PlaySound(_buttonClick, transform.position);
        GameManager.Instance.LoadSettings();
    }
}
