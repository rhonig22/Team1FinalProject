using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBookController : MonoBehaviour
{
    [SerializeField] private GameObject _recipeButtonPrefab;
    [SerializeField] private ScriptableRecipe[] _recipesList;
    private readonly float _yOffset = 125f;
    private float _currentOffset = 200f;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRecipeList();
    }

    private void GenerateRecipeList()
    {
        foreach (var recipe in _recipesList)
        {
            var button = GenerateRecipeButton(recipe);
            button.transform.localPosition = new Vector3(0, _currentOffset, 0);
            _currentOffset -= _yOffset;
        }
    }

    private GameObject GenerateRecipeButton(ScriptableRecipe recipe)
    {
        var button = Instantiate(_recipeButtonPrefab, transform);
        button.GetComponent<RecipeButtonController>().SetRecipe(recipe);
        return button;
    }
}
