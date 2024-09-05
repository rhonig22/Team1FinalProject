using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XboxButtonController : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private string _buttonKey;
    [SerializeField] private AudioClip _buttonClick;

    private void Start()
    {
        _button.onClick.AddListener(() => SoundManager.Instance.PlaySound(_buttonClick, transform.position));
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(_buttonKey) && Input.GetButtonDown(_buttonKey))
            _button.onClick.Invoke();
    }
}
