
using UnityEngine;

[CreateAssetMenuAttribute (fileName = "New Ingredient", menuName = "Ingredients/ New Ingredient")]
public class ScriptableIngredient : ScriptableObject
{
    [SerializeField] private string _ingredientName;
    [SerializeField] private GameObject _ingredientPrefab;
    [SerializeField] private Sprite[] _ingredientSprites;

    public string GetName()
    {
        return _ingredientName;
    }

    public Sprite getSprite(int index)
    {
        if (_ingredientSprites == null || _ingredientSprites.Length == 0)
            return null;
        else if (index < _ingredientSprites.Length)
            return _ingredientSprites[index];
        else
            return _ingredientSprites[_ingredientSprites.Length - 1];
    }

    public GameObject getPrefab()
    {
        return _ingredientPrefab;
    }
}
