using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScrollObject : MonoBehaviour
{
    [SerializeField] private string _buttonName = "XButton";
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _inactiveSprite;
    [SerializeField] private Sprite _missedSprite;
    [SerializeField] private AudioClip _noteClip;

    private readonly float _noteHitHeight = .4f;
    private readonly float _goodNoteHeight = .2f;
    private readonly float _perfectNoteHeight = .1f;
    private float _rhythmControllerLocation = 0f;
    private bool _wasPressed = false;
    public bool CanBePressed { get; private set; } = false;
    public HitType HitType { get; private set; } = HitType.Upcoming;

    private void Start()
    {
        _rhythmControllerLocation = GameObject.FindGameObjectWithTag("RhythmController").transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (HitType == HitType.Missed)
            return;

        float currentCenter = transform.position.y;
        if (currentCenter >= _rhythmControllerLocation - _noteHitHeight &&
            currentCenter <= _rhythmControllerLocation + _noteHitHeight)
        {
            CanBePressed = true;
            HitType = HitType.Normal;
            if (currentCenter >= _rhythmControllerLocation - _goodNoteHeight &&
                currentCenter <= _rhythmControllerLocation + _goodNoteHeight)
            {
                HitType = HitType.Good;
            }

            if (currentCenter >= _rhythmControllerLocation - _perfectNoteHeight &&
                currentCenter <= _rhythmControllerLocation + _perfectNoteHeight)
            {
                HitType = HitType.Perfect;
            }
        }
        else if (currentCenter < _rhythmControllerLocation - _noteHitHeight)
        {
            CanBePressed = false;
            HitType = HitType.Missed;
            SetMissed();
        }
        else
        {
            CanBePressed = false;
        }
    }

    public void PlayNote()
    {
        SoundManager.Instance.PlaySound(_noteClip, transform.position);
    }

    public void SetInactive()
    {
        _spriteRenderer.sprite = _inactiveSprite;
        _wasPressed = true;
    }

    public void SetMissed()
    {
        if (!_wasPressed)
            _spriteRenderer.sprite = _missedSprite;
    }

    public string GetButton() { return _buttonName; }
}
