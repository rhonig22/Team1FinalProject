using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeBookController : MonoBehaviour
{
    [SerializeField] private GameObject _pagePrefab;
    [SerializeField] private GameObject _recipeButtonPrefab;
    [SerializeField] private GameObject _starControllerPrefab;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    private Transform _recipeListArea;
    private Transform _recipeScoresArea;
    [SerializeField] private ScriptableRecipe[] _recipesList;
    private readonly float _yOffset = 125f;
    private readonly float _initialOffset = 200f;
    private readonly int _recipesPerPage = 4;
    private float _currentOffset = 200f;
    private int _currentPage = 0;
    private List<GameObject> _pages = new List<GameObject>();
    public bool NeedToRetry { get; private set; } = false;
    private static int _unlockCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRecipeList();
    }

    private void GenerateRecipeList()
    {
        var prevUnlockCount = _unlockCount;
        _unlockCount = 0;
        for (int i = 0; i < _recipesList.Length; i++)
        {
            var recipe = _recipesList[i];
            if (i % _recipesPerPage == 0)
            {
                CreatePage(i == 0);
                _currentOffset = _initialOffset;
            }

            var button = GenerateRecipeButton(recipe);
            button.transform.localPosition = new Vector3(0, _currentOffset, 0);
            if (i == 0)
            {
                if (!RecipeUXController.DontSelectRecipe)
                {
                    EventSystem.current.SetSelectedGameObject(button);
                }
            }

            var recipeEntry = SaveDataManager.Instance.GetRecipeEntry(recipe.GetName());
            if (recipeEntry == null)
            {
                recipeEntry = SaveDataManager.Instance.InitializeRecipeEntry(recipe.GetName(), true);
            }

            var stars = GenerateStarController(recipeEntry, recipe.GetMaxScore());
            stars.transform.localPosition = new Vector3(0, _currentOffset, 0);
            _currentOffset -= _yOffset;
        }

        if (_unlockCount < _recipesList.Length && _unlockCount == prevUnlockCount && _unlockCount == SaveDataManager.Instance.CountPlayedRecipes())
            NeedToRetry = true;

        SetPageButtonStates(false);
    }

    private void CreatePage(bool isFirstPage)
    {
        var page = Instantiate(_pagePrefab, transform);
        _pages.Add(page);
        _recipeListArea = page.transform.GetChild(0);
        _recipeScoresArea = page.transform.GetChild(1);
        if (!isFirstPage)
            page.SetActive(false);
    }

    public void PageLeft()
    {
        if (_currentPage > 0)
        {
            _pages[_currentPage].SetActive(false);
            _currentPage--;
            _pages[_currentPage].SetActive(true);
            SetPageButtonStates();
        }
    }

    public void PageRight()
    {
        if (_currentPage + 1 < _pages.Count)
        {
            _pages[_currentPage].SetActive(false);
            _currentPage++;
            _pages[_currentPage].SetActive(true);
            SetPageButtonStates();
        }
    }

    private void SetPageButtonStates(bool selectButton = true)
    {
        bool isLeftEnabled = _currentPage > 0;
        _leftButton.interactable = isLeftEnabled;
        bool isRightEnabled = _currentPage + 1 < _pages.Count;
        _rightButton.interactable = isRightEnabled;
        if (selectButton)
        {
            SelectCurrentPageButton();
        }
    }

    public void SelectCurrentPageButton()
    {
        var recipe = _pages[_currentPage].GetComponentInChildren<RecipeButtonController>();
        if (recipe != null && recipe.IsUnlocked)
        {
            var button = _pages[_currentPage].GetComponentInChildren<Button>();
            EventSystem.current.SetSelectedGameObject(button.gameObject);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(_leftButton.gameObject);
        }
    }

    private GameObject GenerateRecipeButton(ScriptableRecipe recipe)
    {
        var button = Instantiate(_recipeButtonPrefab, _recipeListArea);
        var buttonController = button.GetComponent<RecipeButtonController>();
        buttonController.SetRecipe(recipe);
        if (buttonController.IsUnlocked)
            _unlockCount++;

        return button;
    }

    private GameObject GenerateStarController(RecipeEntry entry, int maxScore)
    {
        var stars = Instantiate(_starControllerPrefab, _recipeScoresArea);
        var starController = stars.GetComponent<StarController>();
        starController.SetMaxScore(maxScore);
        starController.SetScore(entry.HighScore);
        return stars;
    }

    public static void ClearRetryMessage()
    {
        _unlockCount = 0;
    }
}
