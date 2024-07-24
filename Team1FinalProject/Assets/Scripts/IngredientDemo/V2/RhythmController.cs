using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _color;
    [SerializeField] private Color _pressedColor;
    [SerializeField] private List<NoteScrollObject> _notes;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_notes.Count > 0 && _notes[0].HitType == HitType.Missed)
        {
            RemoveNote(_notes[0]);
        }

        if (_notes.Count == 0 || !NoteManager.Instance.IsBeatStarted)
        {
            return;
        }

        var note = _notes[0];
        if (Input.GetButtonDown(note.Button))
        {
            _spriteRenderer.color = _pressedColor;

            if (note.CanBePressed)
            {
                NoteManager.Instance.NoteHit(note.HitType);
                RemoveNote(note);
            }
            else
            {
                NoteManager.Instance.NoteMissed();
            }
        }

        if (Input.GetButtonUp(note.Button))
        {
            _spriteRenderer.color = _color;
        }
    }

    private void RemoveNote(NoteScrollObject note)
    {
        note.gameObject.SetActive(false);
        _notes.Remove(note);
    }
}
