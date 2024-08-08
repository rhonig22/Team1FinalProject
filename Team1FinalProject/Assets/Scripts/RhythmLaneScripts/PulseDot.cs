
using UnityEngine;
using UnityEngine.UI;

public class PulseDot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _beatSprites;
    private int index = 0;

    private void Start()
    {
        _spriteRenderer.sprite = _beatSprites[0];
        NoteManager.Instance.BeatEvent.AddListener((int beat) => { index = beat % _beatSprites.Length; });
    }

   
    void Update()
    {
        _spriteRenderer.sprite = _beatSprites[index];
    }
}
