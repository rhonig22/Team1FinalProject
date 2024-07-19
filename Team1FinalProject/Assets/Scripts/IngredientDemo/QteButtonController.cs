using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QteButtonController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private KeyCode _key;
    [SerializeField] private Color _color;
    [SerializeField] private Color _pressedColor;
    [SerializeField] private List<NoteObject> _notes;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_key))
        {
            _spriteRenderer.color = _pressedColor;
            if (_notes.Count > 0 && _notes[0].CanBePressed) {
                var note = _notes[0];
                note.gameObject.SetActive(false);
                _notes.RemoveAt(0);
                GameManager.Instance.NoteHit(note.HitType);
            }
        }

        if (Input.GetKeyUp(_key))
        {
            _spriteRenderer.color = _color;
        }
    }
}
