using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    private float _beatTempo;
    public bool HasStopped { get; private set; }
    [SerializeField] private float _initialSize;

    private void Start()
    {
        _beatTempo = NoteManager.Instance.BeatTempo / 60f;
        ResetTransform();
    }

    private void FixedUpdate()
    {
        if (HasStopped)
            return;

        if (NoteManager.Instance.IsBeatStarted)
        {
            transform.localScale -= new Vector3(_beatTempo * Time.fixedDeltaTime, _beatTempo * Time.fixedDeltaTime, 0f);

            if (transform.localScale.x <= 0)
            {
                HasStopped = true;
            }
        }
    }

    private void ResetTransform()
    {
        transform.localScale = new Vector3(_initialSize, _initialSize, 0f);
    }
}
