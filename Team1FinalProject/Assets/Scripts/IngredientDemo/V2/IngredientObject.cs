using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientObject : MonoBehaviour
{
    public string IngredientName;
    public List<NoteScrollObject> Notes = new List<NoteScrollObject>();
    private float _minYThreshold = -10f;
    [SerializeField] private int _measureCount = 1;

    private void Update()
    {
        if (transform.position.y <= _minYThreshold)
        {
            Destroy(gameObject);
        }
    }

    public int GetMeasureCount () { return _measureCount; }
}
