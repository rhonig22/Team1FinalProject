using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitTextUXController : MonoBehaviour
{
    [SerializeField] private RectTransform _textContainer;

    public void SetLocation(Vector3 pos)
    {
        _textContainer.localPosition = pos;
    }
}
