using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseToTheSuccessfulHit : MonoBehaviour
{
    [SerializeField] float _pulseSize = 1.5f;
    [SerializeField] float _returnSpeed = 5f;
    private Vector3 _startSize;
    void Start()
    {
        _startSize = transform.localScale;
        //When you hear a ok/good/great, expand
        NoteManager.Instance.SuccessfulHitEvent.AddListener((int hit) => { transform.localScale = _startSize * _pulseSize; });
    }

    // Update is called once per frame
    void Update()
    {
        //Shrink w/Lerp
        transform.localScale = Vector3.Lerp(transform.localScale, _startSize, Time.deltaTime * _returnSpeed);
    }
}
