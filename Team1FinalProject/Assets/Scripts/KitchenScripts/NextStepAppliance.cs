using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStepAppliance : MonoBehaviour
{
    [SerializeField] private Station _station;
    [SerializeField] float _pulseSize = 1.05f;
    [SerializeField] float _returnSpeed = 4f;
    [SerializeField] GameObject _glow;
    [SerializeField] GameObject _sparkles;
    [SerializeField] List<Transform> _sparklePositions = new List<Transform>();
    private readonly float _sparkleDelay = .02f;
    private int _positionIndex = 0;
    private List<Animator> _sparkleAnimators = new List<Animator>();
    private Vector3 _startSize;

    // Start is called before the first frame update
    void Start()
    {
        _startSize = transform.localScale;
        for (var i = 0; i < _sparklePositions.Count; i++) {
            var sparkle = Instantiate(_sparkles, transform);
            _sparkleAnimators.Add(sparkle.GetComponent<Animator>());
        }

        NoteManager.Instance.BeatEvent.AddListener((int beat) => 
        {
            var step = RecipeManager.Instance.GetNextStep();
            if (step != null && step.Station == _station)
            {
                transform.localScale = _startSize * _pulseSize;

                StartCoroutine(TriggerSparkles());
            }
        });

    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _startSize, Time.deltaTime * _returnSpeed);
        var step = RecipeManager.Instance.GetNextStep();
        if (step != null && step.Station == _station)
            _glow.SetActive(true);
        else
            _glow.SetActive(false);
    }

    private IEnumerator TriggerSparkles()
    {
        int typeIndex = 0;
        foreach (var sparkleAnimator in _sparkleAnimators)
        {
            sparkleAnimator.transform.localPosition = _sparklePositions[_positionIndex++].localPosition;
            sparkleAnimator.SetTrigger("Sparkle" + (typeIndex + 1));
            typeIndex = (typeIndex + 1) % 3;
            _positionIndex %= _sparklePositions.Count;
            yield return new WaitForSeconds(_sparkleDelay);
        }
    }
}
