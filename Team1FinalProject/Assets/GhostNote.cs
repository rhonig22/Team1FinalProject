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
    private float _noteHitHeight = .4f;

    void Start()
    {
        _rhythmControllerLocation = GameObject.FindGameObjectWithTag("RhythmController").transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float currentCenter = transform.position.y;

        if (currentCenter <= _rhythmControllerLocation)
        {
            SoundManager.Instance.PlaySound(_noteClip, transform.position);
            MonoBehaviour.Destroy(gameObject);
        }

    }
}
