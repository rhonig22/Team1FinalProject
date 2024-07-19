using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    private float _noteHitWidth = .4f;
    private float _goodNoteWidth = .2f;
    private float _perfectNoteWidth = .1f;
    private float _noteSize = 1f;
    public bool CanBePressed { get; private set; } = false;
    public HitType HitType { get; private set; } = HitType.Upcoming;

    // Update is called once per frame
    void Update()
    {
        float currentSize = transform.localScale.x;
        float upperBound = _noteSize + _noteHitWidth;
        float lowerBound = _noteSize - _noteHitWidth;
        if (currentSize >= lowerBound &&
            currentSize <= upperBound)
        {
            CanBePressed = true;
            HitType = HitType.Normal;
            if (currentSize >= _noteSize - _goodNoteWidth &&
                currentSize <= _noteSize + _goodNoteWidth)
            {
                HitType = HitType.Good;
            }

            if (currentSize >= _noteSize - _perfectNoteWidth &&
                currentSize <= _noteSize + _perfectNoteWidth)
            {
                HitType = HitType.Perfect;
            }
        }
        else if (currentSize < lowerBound)
        {
            CanBePressed = false;
            HitType = HitType.Missed;
        }
        else
        {
            CanBePressed = false;
        }
    }
}
