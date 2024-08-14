using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class InteractableAppliance : MonoBehaviour
{
    private bool _isInRange;
    [SerializeField] private string _interactKey;
    [SerializeField] private bool _RightOrUp;
    [SerializeField] private UnityEvent _interactAction;
    [SerializeField] private Station _station;
    void Update()
    {
        if (_isInRange && !KitchenCanvasController.IsRhythmSection)
        {
          /*  if (
                (Input.GetAxis(_interactKey) > 0f && _RightOrUp) //up and right
                || (Input.GetAxis(_interactKey) < 0f && !_RightOrUp) //down and left
                || Input.GetKeyDown(KeyCode.W))
            {
                //W is temporary for people w/o a controller till we get 
                //up/down/left/right or wasd for the arrowkeyless
                _interactAction.Invoke();
            }
          */
        }
    }

    public void StartNextRecipeStep()
    {
        
        RecipeStep step = RecipeManager.Instance.GetNextStep();
        UnityEngine.Debug.Log("Starting next recipe step: " + step);
        if (step.Station == _station)
            LaneScroller.Instance.AddToIngredientQueue(step.Ingredient.getPrefab());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UnityEngine.Debug.Log("Entered Collision");
        StartNextRecipeStep();
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