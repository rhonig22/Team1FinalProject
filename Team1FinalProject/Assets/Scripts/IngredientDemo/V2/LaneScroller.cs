using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneScroller : MonoBehaviour
{
    public static LaneScroller Instance;

    [SerializeField] private GameObject _emptyIngredientPrefab;
    [SerializeField] private GameObject _onionIngredientPrefab;
    private float _beatTempo;
    private float _currentTop = 4f;
    private int _measureCount = 0;
    private float _timeToNextMeasure = 0;
    private readonly float _measureSize = 4f;
    private List<NoteScrollObject> _notes = new List<NoteScrollObject>();
    private List<GameObject> _ingredientQueue = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _beatTempo = NoteManager.Instance.BeatTempo / 60f;
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
        return _notes.Count > 0 || _ingredientQueue.Count > 0;
    }

    public NoteScrollObject GetNextNote()
    {
        if (_notes.Count > 0)
            return _notes[0];

        return null;
    }

    public void RemoveNote(NoteScrollObject note)
    {
        _notes.Remove(note);
    }

    public void AddToIngredientQueue(GameObject ingredientPrefab)
    {
        _ingredientQueue.Add(ingredientPrefab);
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
        var ingredient = Instantiate(nextIngredientPrefab, new Vector3(transform.position.x, transform.position.y + _currentTop, 0f), transform.rotation);
        ingredient.transform.parent = transform;
        IngredientObject ingredientObj = ingredient.GetComponent<IngredientObject>();
        _notes.AddRange(ingredientObj.Notes);
    }
}
