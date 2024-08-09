using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenBackgroundRenderer : MonoBehaviour
{
    [SerializeField] Sprite _flattop;
    [SerializeField] Sprite _stovetop;
    [SerializeField] Sprite _prep;
    [SerializeField] Sprite _fridge;


    // Start is called before the first frame update
    void Start()
    {
        var flattop = new GameObject("flattop", typeof(SpriteRenderer));
        var spriteRenderer = flattop.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _flattop;
        spriteRenderer.sortingLayerName = "Background";
        spriteRenderer.sortingOrder = 1;
        flattop.transform.localScale = Vector3.one * 5;
        flattop.transform.SetParent(transform);

        var stovetop = new GameObject("stovetop", typeof(SpriteRenderer));
        spriteRenderer = stovetop.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _stovetop;
        spriteRenderer.sortingLayerName = "Background";
        spriteRenderer.sortingOrder = 1;
        stovetop.transform.localScale = Vector3.one * 5;
        stovetop.transform.SetParent(transform);

        var prep = new GameObject("prep", typeof(SpriteRenderer));
        spriteRenderer = prep.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _prep;
        spriteRenderer.sortingLayerName = "Background";
        spriteRenderer.sortingOrder = 1;
        prep.transform.localScale = Vector3.one * 5;
        prep.transform.SetParent(transform);

        var fridge = new GameObject("fridge", typeof(SpriteRenderer));
        spriteRenderer = fridge.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _fridge;
        spriteRenderer.sortingLayerName = "Background";
        spriteRenderer.sortingOrder = 1;
        fridge.transform.localScale = new Vector3(-5, 5, 5);
        fridge.transform.SetParent(transform);
    }
}
