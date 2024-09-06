using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GhostNote : MonoBehaviour
{
    [SerializeField] private AudioClip _noteClip;

    private float _rhythmControllerLocation = 0f;
    private bool _playingClip = false;
    private AudioSource _audioSource;
    private bool _missedNote = false;
    

    void Start()
    {
        //listen for button hit and button miss
        NoteManager.Instance.SuccessfulHitEvent.AddListener((NoteScrollObject note) => { _missedNote = false; });
        NoteManager.Instance.MissedHitEvent.AddListener((int mNotes) => { _missedNote = true; });
        _rhythmControllerLocation = GameObject.FindGameObjectWithTag("RhythmController").transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float currentCenter = transform.position.y;

        if (currentCenter <= _rhythmControllerLocation && !_playingClip)
        {
            _playingClip = true;
            _audioSource = SoundManager.Instance.PlayAdjustableSound(_noteClip, transform.position);
            //MonoBehaviour.Destroy(gameObject);
        }
        else if (_audioSource != null)
        {
            if(_missedNote)
            {
                //Debug.Log("Miss");
                _audioSource.volume = (.1f * SoundManager.Instance.GetCurrentVolume());
            }
            else
            {
                _audioSource.volume = SoundManager.Instance.GetCurrentVolume();
                //Debug.Log("Hit");
            }
                
        }

    }
}
