
using UnityEngine;

[CreateAssetMenuAttribute (fileName = "New Ingredient", menuName = "Ingredients/ New Ingredient")]
public class ScriptableIngredient : ScriptableObject
{
    [SerializeField] private string _ingredientName;
    [SerializeField] private GameObject _ingredientPrefab;
    [SerializeField] private Sprite _ingredientSprite;

    public string GetName()
    {
        return _ingredientName;
    }

    public Sprite getSprite()
    {
        return _ingredientSprite;
    }

    public GameObject getPrefab()
    {
        return _ingredientPrefab;
    }
}
