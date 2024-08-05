using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableAppliance : MonoBehaviour
{
    private bool _isInRange;
    [SerializeField] private string _interactKey;
    [SerializeField] private UnityEvent _interactAction;
    [SerializeField] private Station _station;
    void Update()
    {
        if(_isInRange && !KitchenCanvasController.IsRhythmSection)  
        {
            if(Input.GetButtonDown(_interactKey))
            {
                _interactAction.Invoke();
            }
        }
    }

    public void StartNextRecipeStep()
    {
        RecipeStep step = RecipeManager.Instance.GetNextStep();
        if (step.Station == _station)
            LaneScroller.Instance.AddToIngredientQueue(step.Ingredient.getPrefab());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _isInRange = false;
        }
    }
}