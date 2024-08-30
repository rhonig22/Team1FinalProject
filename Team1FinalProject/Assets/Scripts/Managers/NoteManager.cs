using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance;
    [SerializeField] private AudioClip _noteHitClip;
    [SerializeField] private AudioClip _noteMissedClip;
    [SerializeField] private GameObject _normalHitMessage;
    [SerializeField] private GameObject _goodHitMessage;
    [SerializeField] private GameObject _perfectHitMessage;
   
    public float BeatTempo { get; private set; }
    public UnityEvent<int> BeatEvent { get; private set; } = new UnityEvent<int>();
    public UnityEvent<NoteScrollObject> SuccessfulHitEvent { get; private set; } = new UnityEvent<NoteScrollObject>();
    public UnityEvent<int> MissedHitEvent { get; private set; } = new UnityEvent<int>();

    public readonly float NoteLoopSize = 31.5f;
    public readonly int NormalNotePoints = 120;
    public readonly int GoodNotePoints = 140;
    public readonly int PerfectNotePoints = 150;
    public bool IsBeatStarted { get; private set; } = false;
    public int Score { get; private set; } = 0;
    public int NotesHit { get; private set; } = 0;
    public int NotesMissed { get; private set; } = 0;
    private Vector3 _messageOffset = new Vector3(-275f, 0, 1);
    private Vector3 _messagePlacement = new Vector3(-320f, -275f, 1);

    private int _lastBeat;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        //If a whole "beat" in 60/bpm has elapsed(Plus a bit of float cruft, "Do things on the beat")
        if (MusicManager.Instance == null)
            return;

        var musicSource = MusicManager.Instance.getMusicSource();
        if (musicSource == null)
        {
            return;
        }
        else
        {
            if (musicSource.isPlaying && IsBeatStarted)
            {
                float sampledTime = (musicSource.timeSamples / (musicSource.clip.frequency * GetBeatLength()));
                if (CheckForNewBeat(sampledTime))
                {
                    BeatEvent.Invoke(_lastBeat);
                }
            }
        }
    }

    public void StopBeats()
    {
        MusicManager.Instance.StopMusic();
        IsBeatStarted = false;
        Score = 0;
    }

    public void StartBeats()
    {
        BeatTempo = RecipeManager.Instance.GetBPM();
        MusicManager.Instance.PlayMusicClip(RecipeManager.Instance.GetBackingTrack());
        ResetScore();
        IsBeatStarted = true;
    }
    public float GetBeatLength()
    {
        return 60f / BeatTempo;
    }

    public bool CheckForNewBeat(float beat)
    {
        if (Mathf.FloorToInt(beat) != _lastBeat)
        {
            _lastBeat = Mathf.FloorToInt(beat);
            return true;
        }
        return false;
    }

    public void NoteHit(NoteScrollObject note)
    {
        HitType type = note.HitType;
        NotesHit++;

        SuccessfulHitEvent.Invoke(note);
        RecipeManager.Instance.IncrementIngredientSprite();

        GameObject hitText = null;
        switch (type)
        {
            case HitType.Normal:
                Score += NormalNotePoints;
                hitText = Instantiate(_normalHitMessage, _normalHitMessage.transform.position, _normalHitMessage.transform.rotation);
                break;
            case HitType.Good:
                Score += GoodNotePoints;
                hitText = Instantiate(_goodHitMessage, _goodHitMessage.transform.position, _goodHitMessage.transform.rotation);
                break;
            case HitType.Perfect:
                Score += PerfectNotePoints;
                hitText = Instantiate(_perfectHitMessage, _perfectHitMessage.transform.position, _perfectHitMessage.transform.rotation);
                break;
            case HitType.Missed:
            case HitType.Upcoming:
            default:
                break;
        }

        if (hitText != null) {
            
            var hitTextController = hitText.GetComponent<HitTextUXController>();
            hitTextController.SetLocation(_messagePlacement);
            
        }

     
    }

    public void NoteMissed()
    {

        SoundManager.Instance.PlaySound(_noteMissedClip, Vector3.zero);
        NotesMissed++;
        MissedHitEvent.Invoke(NotesMissed);
        //Debug.Log("REALLY  missed");
    }

    private void ResetScore()
    {
        Score = 0;
        NotesHit = 0;
        NotesMissed = 0;
    }
}

public enum HitType
{
    Upcoming,
    Missed,
    Normal,
    Good,
    Perfect
}

