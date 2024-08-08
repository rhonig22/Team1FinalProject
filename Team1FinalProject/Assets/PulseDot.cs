
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

   
  void Update()
    {
        _spriteRenderer.sprite = _beatSprites[index % _beatSprites.Length];
    }


}
