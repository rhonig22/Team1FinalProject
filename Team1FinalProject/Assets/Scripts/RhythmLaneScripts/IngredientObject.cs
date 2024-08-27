using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientObject : MonoBehaviour
{
    public string IngredientName;
    private readonly float _minYThreshold = -6f;
    private readonly float _measureSize = 4f;
    [SerializeField] private int _measureCount = 1;

    private void Update()
    {
        if (transform.position.y <= _minYThreshold - _measureCount * _measureSize)
        {
            Destroy(gameObject);
        }
    }

    public int GetMeasureCount () { return _measureCount; }

    //only works if we are actually using the "blank" notes everywhere and I'm not sure we are.
    public int GetNoteCount () { return transform.childCount - (_measureCount * 4); }
}
