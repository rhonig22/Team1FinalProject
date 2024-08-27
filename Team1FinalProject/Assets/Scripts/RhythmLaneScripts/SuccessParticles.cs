using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        NoteManager.Instance.SuccessfulHitEvent.AddListener((NoteScrollObject note) =>
        {
            _particleSystem.Play();
        });
    }
}
