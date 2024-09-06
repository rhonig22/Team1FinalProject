using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoteCursorButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
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

    private void OnDisable()
    {
        if (_noteCursor != null)
            _noteCursor.SetActive(false);
    }
}
