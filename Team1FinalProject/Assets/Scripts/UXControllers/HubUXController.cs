using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubUXController : MonoBehaviour
{
    public void RecipeBookClicked()
    {
        GameManager.Instance.LoadRecipeBook();
    }
}
