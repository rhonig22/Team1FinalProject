using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientStore : MonoBehaviour
{
    [SerializeField] ScriptableIngredient _egg;
    [SerializeField] ScriptableIngredient _salt;
    [SerializeField] ScriptableIngredient _slicedOnion;
    [SerializeField] ScriptableIngredient _dicedOnion;

    public void SendEgg()
    {
        LaneScroller.Instance.AddToIngredientQueue(_egg.getPrefab());
    }

    public void SendSalt()
    {
        LaneScroller.Instance.AddToIngredientQueue(_salt.getPrefab());
    }

    public void SendSlicedOnion()
    {
        LaneScroller.Instance.AddToIngredientQueue(_slicedOnion.getPrefab());
    }

    public void SendDicedOnion()
    {
        LaneScroller.Instance.AddToIngredientQueue(_dicedOnion.getPrefab());
    }
}
