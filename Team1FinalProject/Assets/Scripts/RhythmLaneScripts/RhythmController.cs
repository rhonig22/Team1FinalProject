using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _color;
    [SerializeField] private Color _pressedColor;
    [SerializeField] private LaneScroller _laneScroller;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_laneScroller.HasNotes() || !NoteManager.Instance.IsBeatStarted)
        {
            return;
        }

        var note = _laneScroller.GetNextNote();
        if (Input.GetButtonDown(note.GetButton()))
        {
            _spriteRenderer.color = _pressedColor;

            if (note.CanBePressed)
            {
                NoteManager.Instance.NoteHit(note.HitType);
                note.PlayNote();
                //particle effect?
                note.SetInactive();
                _laneScroller.RemoveNote(note);
            }
            else
            {
                NoteManager.Instance.NoteMissed();
            }
        }

        if (Input.GetButtonUp(note.GetButton()))
        {
            _spriteRenderer.color = _color;
        }
    }
}
