using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeUXController : MonoBehaviour
{
    public void MainMenuClicked()
    {
        GameManager.Instance.LoadTitleScreen();
    }

    public void Recipe1Clicked()
    {
        GameManager.Instance.LoadKitchen();
    }
}
