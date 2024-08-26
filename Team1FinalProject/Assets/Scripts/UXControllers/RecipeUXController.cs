using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeUXController : MonoBehaviour
{
    [SerializeField] GameObject _unlockScreen;
    [SerializeField] TextMeshProUGUI _unlockText;

    private void Start()
    {
        var unlocks = CustomizationManager.Instance.CheckUnlocks();
        if (unlocks.Count > 0)
        {
            string newText = string.Empty;
            foreach (var unlock in unlocks)
            {
                newText += unlock.GetName() + "\r\n";
            }

            _unlockText.text = newText;
            _unlockScreen.SetActive(true);
        }
    }

    public void MainMenuClicked()
    {
        GameManager.Instance.LoadTitleScreen();
    }

    public void CloseUnlockScreen()
    {
        _unlockScreen.SetActive(false); 
    }
}
