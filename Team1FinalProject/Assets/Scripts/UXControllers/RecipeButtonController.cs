using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeButtonController : MonoBehaviour
{
    [SerializeField] private ScriptableRecipe _recipe;

    public void RecipeClicked()
    {
        RecipeManager.Instance.SetRecipe(_recipe);
        GameManager.Instance.LoadKitchen();
    }
}
