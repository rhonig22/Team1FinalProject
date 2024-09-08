using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapKeyboardButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite _controllerSprite;
    [SerializeField] private Sprite _keyboardSprite;

    private void Start()
    {
        if (GameManager.IsController)
        {
            _buttonImage.sprite = _controllerSprite;
        }
        else
        {
            _buttonImage.sprite = _keyboardSprite;
        }
    }
}
