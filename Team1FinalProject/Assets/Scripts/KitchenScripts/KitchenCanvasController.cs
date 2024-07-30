using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenCanvasController : MonoBehaviour
{
    [SerializeField] private GameObject kitchenDimmer;

    public static bool IsRhythmSection = false;

    private void Update()
    {
        IsRhythmSection = LaneScroller.Instance.HasUpcomingNotes();
        kitchenDimmer.SetActive(LaneScroller.Instance.HasUpcomingNotes());
    }
}
