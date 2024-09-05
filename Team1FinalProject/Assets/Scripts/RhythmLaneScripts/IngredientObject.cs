using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientObject : MonoBehaviour
{
    public string IngredientName;
    private float _minYThreshold = -6f;
    private float _measureSize = 4f;
    [SerializeField] private int _measureCount = 1;

    private void Start()
    {
        if (NoteManager.Instance.IsDoubleTime) {
            _measureSize *= NoteManager.Instance.DoubleTimeFactor;
            _minYThreshold *= NoteManager.Instance.DoubleTimeFactor;
        }
    }

    private void Update()
    {
        if (transform.position.y <= _minYThreshold - _measureCount * _measureSize)
        {
            Destroy(gameObject);
        }
    }

    public int GetMeasureCount () { return _measureCount; }

    public int GetNoteCount ()
    {
        return transform.GetComponentsInChildren<NoteScrollObject>().Length;
    }
}
