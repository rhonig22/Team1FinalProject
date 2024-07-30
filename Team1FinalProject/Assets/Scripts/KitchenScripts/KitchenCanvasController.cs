using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject kitchenDimmer;

    public static bool IsRhythmSection = false;

    private void Update()
    {
        IsRhythmSection = LaneScroller.Instance.HasNotes();
        kitchenDimmer.SetActive(LaneScroller.Instance.HasNotes());
    }
}
