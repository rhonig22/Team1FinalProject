using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneScroller : MonoBehaviour
{
    public static LaneScroller Instance;

    [SerializeField] private GameObject _emptyIngredientPrefab;
    private float _beatTempo = 0;
    private float _currentTop = 4f;
    private int _measureCount = 0;
    private float _timeToNextMeasure = 0;
    private  float _measureSize = 4f;
    private readonly float _secondsPerMinute = 60f;
    private List<NoteScrollObject> _notes = new List<NoteScrollObject>();
    private List<GameObject> _ingredientQueue = new List<GameObject>();
    public static int NoteCounter { get; private set; } = 0;
    private bool _isIngredientRunning = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Update()
    {
        if (_notes.Count > 0 && _notes[0].HitType == HitType.Missed)
        {
            RemoveNote(_notes[0]);
        }
    }

    private void FixedUpdate()
    {
        if (NoteManager.Instance.IsBeatStarted && _beatTempo == 0)
        {
            // initialize beat values for the song
            _beatTempo = NoteManager.Instance.BeatTempo / _secondsPerMinute;
            if (NoteManager.Instance.IsDoubleTime)
            {
                _measureSize *= NoteManager.Instance.DoubleTimeFactor;
            }

            SetUpInitialRests();
        }

        if (NoteManager.Instance.IsBeatStarted)
        {
            _timeToNextMeasure += _beatTempo * Time.fixedDeltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y - _beatTempo * Time.fixedDeltaTime, 0f);

            if (_timeToNextMeasure > _measureSize)
            {
                _measureCount++;
                _timeToNextMeasure -= _measureSize;
                _currentTop += _measureSize;
                GetNextIngredient();
            }
        }
    }

    public bool HasNotes()
    {
        return _notes.Count > 0;
    }

    public bool HasUpcomingNotes()
    {
        return _notes.Count > 0 || _ingredientQueue.Count > 0;
    }
    public bool IsIngredientRunning()
    {
        return _isIngredientRunning;
    }

    public NoteScrollObject GetNextNote()
    {
        if (_notes.Count > 0)
            return _notes[0];

        return null;
    }

    public void RemoveNote(NoteScrollObject note)
    {
        var hasNotes = HasUpcomingNotes();
        _notes.Remove(note);
        NoteCounter++;

        if (hasNotes && !HasUpcomingNotes())
        {
            StartCoroutine(DelayedStepIncrement());
        }
    }

    private IEnumerator DelayedStepIncrement()
    {
        yield return new WaitForSeconds(1f);
        RecipeManager.Instance.IncrementStep();
        _isIngredientRunning = false;
    }

    public void AddToIngredientQueue(GameObject ingredientPrefab)
    {
        _ingredientQueue.Add(ingredientPrefab);
        _isIngredientRunning = true;
    }

    public GameObject PopIngredientQueue()
    {
        if (_ingredientQueue.Count > 0)
        {
            var nextIngredientPrefab = _ingredientQueue[0];
            _ingredientQueue.RemoveAt(0);
            return nextIngredientPrefab;
        }

        return _emptyIngredientPrefab;
    }

    private void GetNextIngredient()
    {
        var nextIngredientPrefab = PopIngredientQueue();
        NoteCounter = 1;
        var ingredient = Instantiate(nextIngredientPrefab, new Vector3(transform.position.x, transform.position.y + _currentTop, 0f), transform.rotation);
        ingredient.transform.parent = transform;
        var ingredientNotes = ingredient.GetComponentsInChildren<NoteScrollObject>();
        if (NoteManager.Instance.IsDoubleTime)
        {
            foreach (NoteScrollObject note in ingredientNotes)
                note.transform.localPosition *= NoteManager.Instance.DoubleTimeFactor;

            var rests = ingredient.GetComponentsInChildren<PulseDot>();
            foreach (PulseDot rest in rests)
                rest.transform.localPosition *= NoteManager.Instance.DoubleTimeFactor;

            var ghostNote = ingredient.GetComponentInChildren<GhostNote>();
            if (ghostNote != null)
                ghostNote.transform.localPosition *= NoteManager.Instance.DoubleTimeFactor;
        }

        _notes.AddRange(ingredientNotes);
    }

    private void SetUpInitialRests()
    {
        for (var i = 0; i < 3; i++)
        {
            var ingredient = Instantiate(_emptyIngredientPrefab, new Vector3(transform.position.x, _currentTop - i * _measureSize, 0f), transform.rotation);
            ingredient.transform.parent = transform;
            if (NoteManager.Instance.IsDoubleTime)
            {
                var rests = ingredient.GetComponentsInChildren<PulseDot>();
                foreach (PulseDot rest in rests)
                    rest.transform.localPosition *= NoteManager.Instance.DoubleTimeFactor;
            }
        }
    }
}
