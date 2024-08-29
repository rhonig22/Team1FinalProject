using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomizerUXController : MonoBehaviour
{
    [SerializeField] GameObject _fridgeButton;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_fridgeButton);
    }
}
