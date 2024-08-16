using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStepAppliance : MonoBehaviour
{
    [SerializeField] private Station _station;
    [SerializeField] float _pulseSize = 1.05f;
    [SerializeField] float _returnSpeed = 4f;
    private Vector3 _startSize;
    // Start is called before the first frame update
    void Start()
    {
        _startSize = transform.localScale;
        NoteManager.Instance.BeatEvent.AddListener((int beat) => 
        {
            var step = RecipeManager.Instance.GetNextStep();
            if (step != null && step.Station == _station)
                transform.localScale = _startSize * _pulseSize;
        });

    }

    // Update is called once per frame
    void Update()
    {
            transform.localScale = Vector3.Lerp(transform.localScale, _startSize, Time.deltaTime * _returnSpeed);
    }
}
