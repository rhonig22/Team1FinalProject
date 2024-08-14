using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class InteractableAppliance : MonoBehaviour
{
    private bool _isInRange;
    [SerializeField] private Station _station;
    void Update()
    {

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