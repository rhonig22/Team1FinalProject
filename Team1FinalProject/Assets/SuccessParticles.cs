using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessParticles : MonoBehaviour
    
{
    private bool _emitParticles = false;
    [SerializeField] private ParticleSystem _particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        NoteManager.Instance.SuccessfulHitEvent.AddListener((int notesHit) =>
        {
            _emitParticles = true;//
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(_emitParticles)
        {

            _emitParticles = false;
            
        }
    }
}
