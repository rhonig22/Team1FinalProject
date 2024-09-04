using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class RecipeUXController : MonoBehaviour
{
    [SerializeField] GameObject _unlockScreen;
    [SerializeField] TextMeshProUGUI _unlockText;
    [SerializeField] Image _unlockImage;
    [SerializeField] RecipeBookController _recipeBook;
    public static bool DontSelectRecipe = false;
    private List<ScriptableCustomization> _unlocks;

    private void Start()
    {
        _unlocks = CustomizationManager.Instance.CheckUnlocks();
        if (_unlocks.Count > 0)
        {
            string newText = string.Empty;
            foreach (var unlock in _unlocks)
            {
                newText += unlock.GetName() + "\r\n";
            }

            DontSelectRecipe = true;
            var unlockSprite = _unlocks[0].GetSprite();
            _unlockImage.sprite = unlockSprite;
            _unlockImage.rectTransform.sizeDelta = new Vector2(unlockSprite.rect.width, unlockSprite.rect.height);
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
        _recipeBook.SelectCurrentPageButton();
    }

    public void CloseAndSwapUnlockScreen()
    {
        if (_unlocks.Count > 0)
        {
            CustomizationManager.Instance.SetCurrentCustomization(_unlocks[0].GetItemType(), _unlocks[0].GetAesthetic());
            _unlocks.Clear();
        }

        _unlockScreen.SetActive(false);
        _recipeBook.SelectCurrentPageButton();
    }
}
