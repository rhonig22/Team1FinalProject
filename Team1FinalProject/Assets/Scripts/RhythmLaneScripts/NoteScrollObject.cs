using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mixpanel;

public class NoteScrollObject : MonoBehaviour
{
    [SerializeField] private string _buttonName = "XButton";
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _inactiveSprite;
    [SerializeField] private Sprite _missedSprite;
    [SerializeField] private Sprite _activeSpriteKeyboard;
    [SerializeField] private Sprite _inactiveSpriteKeyboard;
    [SerializeField] private Sprite _missedSpriteKeyboard;
    [SerializeField] private AudioClip _noteClip;
    [SerializeField] private Animator _animator;

    private readonly float _noteHitHeight = .4f;
    private readonly float _goodNoteHeight = .2f;
    private readonly float _perfectNoteHeight = .1f;
    private float _rhythmControllerLocation = 0f;
    private bool _wasPressed = false;
    public bool CanBePressed { get; private set; } = false;
    public HitType HitType { get; private set; } = HitType.Upcoming;
    private Vector3 _startSize;

    private void Start()
    {
        _startSize = transform.localScale;
        _spriteRenderer.sprite = GameManager.IsController ? _activeSprite : _activeSpriteKeyboard;
        NoteManager.Instance.SuccessfulHitEvent.AddListener((NoteScrollObject note) => {
            if (note == this)
                _animator.SetTrigger("Success");
        });
        _rhythmControllerLocation = GameObject.FindGameObjectWithTag("RhythmController").transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localScale = Vector3.Lerp(transform.localScale, _startSize, Time.deltaTime * 5);
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
        else if (!_wasPressed && currentCenter < _rhythmControllerLocation - _noteHitHeight)
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
        if (_noteClip != null)
        {
            SoundManager.Instance.PlaySound(_noteClip, transform.position);
        }

        LogNoteEvent(true);
    }

    public void SetInactive()
    {
        _spriteRenderer.sprite = GameManager.IsController ? _inactiveSprite : _inactiveSpriteKeyboard;
        _wasPressed = true;
    }

    public void SetMissed()
    {
        if (!_wasPressed)
        {
            _spriteRenderer.sprite = GameManager.IsController ? _missedSprite : _missedSpriteKeyboard;
            NoteManager.Instance.NoteMissed();
        }

        LogNoteEvent(false);
    }

    public string GetButton() { return _buttonName; }

    private void LogNoteEvent(bool wasHit)
    {
        var props = new Value();
        props["recipeName"] = RecipeManager.Instance.GetRecipeName();
        props["ingredientName"] = RecipeManager.Instance.GetNextStep().Ingredient.GetName();
        props["noteNumber"] = LaneScroller.NoteCounter;
        props["wasHit"] = wasHit;
        MixpanelLogger.Instance.LogEvent("Note Event", props);
    }
}
