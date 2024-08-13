using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XboxButtonController : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private string _buttonKey;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown(_buttonKey))
            _button.onClick.Invoke();
    }
}
