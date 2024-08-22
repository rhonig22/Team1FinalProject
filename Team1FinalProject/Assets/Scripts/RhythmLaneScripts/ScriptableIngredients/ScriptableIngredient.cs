
using UnityEngine;

[CreateAssetMenuAttribute (fileName = "New Ingredient", menuName = "Ingredients/ New Ingredient")]
public class ScriptableIngredient : ScriptableObject
{
    [SerializeField] private string _ingredientName;
    [SerializeField] private bool _hasAnimation;
    [SerializeField] private string _animationTrigger;
    [SerializeField] private GameObject _ingredientPrefab;
    [SerializeField] private Sprite[] _ingredientSprites;
    [SerializeField] private Sprite[] _backgroundPrepSprites;

    public bool IsAnimatedIngredient()
    {
        return _hasAnimation;
    }

    public string GetAnimationTrigger()
    {
        return _animationTrigger;
    }

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
    public Sprite getBGPrepSprite(int index)
    {
        if (_backgroundPrepSprites == null || _backgroundPrepSprites.Length == 0)
        {

            return null;
        }
        else if (index < _backgroundPrepSprites.Length)
        {
            
;            return _backgroundPrepSprites[index];
        }
        else
        {        
            return _backgroundPrepSprites[_backgroundPrepSprites.Length - 1];
        }
    }
    public GameObject getPrefab()
    {
        return _ingredientPrefab;
    }
}
