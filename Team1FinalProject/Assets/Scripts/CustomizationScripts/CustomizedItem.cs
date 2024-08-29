using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CustomizedItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ItemType _itemType;

    private void Start()
    {
        SetCurrentSprite();
    }

    public void SetCurrentSprite()
    {
        _spriteRenderer.sprite = CustomizationManager.Instance.GetCurrentItem(_itemType).GetSprite();
    }
}
