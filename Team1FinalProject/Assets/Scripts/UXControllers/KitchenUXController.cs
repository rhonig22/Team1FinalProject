using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenUXController : MonoBehaviour
{
    public void MainMenuClicked()
    {
        GameManager.Instance.LoadTitleScreen();
    }
}
