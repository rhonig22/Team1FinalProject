using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShineButton : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        NoteManager.Instance.SuccessfulHitEvent.AddListener((NoteScrollObject note) =>
        {
            _animator.SetTrigger("TimeToShine");
        });
    }
}