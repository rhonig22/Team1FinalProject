using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnlockedUXController : MonoBehaviour
{
    [SerializeField] GameObject _doneButton;

    private void OnEnable()
    {
        SelectDoneButton();
    }

    public void SelectDoneButton() { EventSystem.current.SetSelectedGameObject(_doneButton); }
}
