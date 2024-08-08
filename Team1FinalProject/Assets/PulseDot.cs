
using UnityEngine;
using UnityEngine.UI;

public class PulseDot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _beatSprites;
    public static int index = 0;

    private void Start()
    {
        _spriteRenderer.sprite = _beatSprites[0];
    }

    //this works once D:< 
  void Update()
    {
        if (Input.GetMouseButtonDown(0))
            pulse();
    }
    private void pulse()
    {
        index++;

        if (index >= _beatSprites.Length)
            index = 0;

        _spriteRenderer.sprite = _beatSprites[index];

    }
    

}
