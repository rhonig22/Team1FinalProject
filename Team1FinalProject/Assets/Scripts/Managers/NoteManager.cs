using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance;
    [SerializeField] private AudioClip _noteHitClip;
    [SerializeField] private AudioClip _noteMissedClip;
    [SerializeField] private GameObject _normalHitMessage;
    [SerializeField] private GameObject _goodHitMessage;
    [SerializeField] private GameObject _perfectHitMessage;
    public readonly float BeatTempo = 126;
    public readonly float NoteLoopSize = 31.5f;
    public readonly int NormalNotePoints = 100;
    public readonly int GoodNotePoints = 120;
    public readonly int PerfectNotePoints = 150;
    public bool IsBeatStarted { get; private set; } = false;
    public int Score { get; private set; } = 0;
    public int NotesHit { get; private set; } = 0;
    public int NotesMissed { get; private set; } = 0;
    private Vector3 _messagePlacement = new Vector3(2, 2, 1);

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void StopBeats()
    {
        MusicManager.Instance.StopMusic();
        IsBeatStarted = false;
        Score = 0;
    }

    public void StartBeats()
    {
        MusicManager.Instance.StartMusic();
        IsBeatStarted = true;
    }

    public void NoteHit(HitType type)
    {
        SoundManager.Instance.PlaySound(_noteHitClip, Vector3.zero);
        NotesHit++;
        switch (type)
        {
            case HitType.Normal:
                Score += NormalNotePoints;
                Instantiate(_normalHitMessage, _messagePlacement, _normalHitMessage.transform.rotation);
                break;
            case HitType.Good:
                Score += GoodNotePoints;
                Instantiate(_goodHitMessage, _messagePlacement, _normalHitMessage.transform.rotation);
                break;
            case HitType.Perfect:
                Score += PerfectNotePoints;
                Instantiate(_perfectHitMessage, _messagePlacement, _normalHitMessage.transform.rotation);
                break;
            case HitType.Missed:
            case HitType.Upcoming:
            default:
                break;
        }

        if (NotesHit % 2 == 0)
            _messagePlacement.y *= -1;
        if (NotesHit % 2 == 1)
            _messagePlacement.x *= -1;
    }

    public void NoteMissed()
    {
        SoundManager.Instance.PlaySound(_noteMissedClip, Vector3.zero);
        NotesMissed++;
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