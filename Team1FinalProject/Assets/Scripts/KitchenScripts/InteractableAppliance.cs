using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableAppliance : MonoBehaviour
{
    [SerializeField] private string _interactKey;
    [SerializeField] private bool _RightOrUp;
    [SerializeField] private UnityEvent _interactAction;
    [SerializeField] private Station _station;

    public void StartNextRecipeStep()
    {
        RecipeStep step = RecipeManager.Instance.GetNextStep();
        if (step != null && step.Station == _station)
            LaneScroller.Instance.AddToIngredientQueue(step.Ingredient.getPrefab());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !KitchenCanvasController.IsRhythmSection)
        {
            _interactAction.Invoke();
            collision.gameObject.GetComponent<PlayerManager>().SameStationAgain.AddListener(() => { _interactAction.Invoke(); });
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !KitchenCanvasController.IsRhythmSection)
        {
            collision.gameObject.GetComponent<PlayerManager>().SameStationAgain.RemoveAllListeners();
        }
    }
}