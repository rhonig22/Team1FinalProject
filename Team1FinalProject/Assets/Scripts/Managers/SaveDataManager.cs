using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager Instance;
    private readonly string _playerDataKey = "PlayerData";
    private readonly string _recipeDataKey = "RecipeData";
    private PlayerData _playerData;
    private RecipeData _recipeData;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        SetUpDataManager();
    }

    private void SetUpDataManager()
    {
        if (PlayerPrefs.HasKey(_playerDataKey))
        {
            _playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(_playerDataKey));
        }
        else
        {
            InitializePlayerData();
        }

        if (PlayerPrefs.HasKey(_recipeDataKey))
        {
            _recipeData = JsonUtility.FromJson<RecipeData>(PlayerPrefs.GetString(_recipeDataKey));
        }
        else
        {
            InitializeRecipeData();
        }
    }

    public PlayerData GetPlayerData()
    {
        return _playerData;
    }

    public void SetPlayerData(PlayerData playerData)
    {
        PlayerPrefs.SetString(_playerDataKey, JsonUtility.ToJson(playerData));
        PlayerPrefs.Save();
    }

    public RecipeData GetAllRecipeData()
    {
        return _recipeData;
    }

    public RecipeEntry GetRecipeEntry(string recipeName)
    {
        foreach (var recipe in _recipeData.RecipeList) {
            if (recipe.Name == recipeName)
            {
                return recipe;
            }
        }

        return null;
    }

    public void SetRecipeEntryData(RecipeEntry recipeEntry)
    {
        bool found = false;
        foreach (var recipe in _recipeData.RecipeList)
        {
            if (recipe.Name == recipeEntry.Name)
            {
                recipe.Stars = recipeEntry.Stars;
                recipe.HighScore = recipeEntry.HighScore;
                recipe.Unlocked = recipeEntry.Unlocked;
                found = true;
                break;
            }
        }

        if (!found)
        {
            _recipeData.RecipeList.Add(recipeEntry);
        }

        SetRecipeData();
    }

    public void SetRecipeData()
    {
        PlayerPrefs.SetString(_recipeDataKey, JsonUtility.ToJson(_recipeData));
        PlayerPrefs.Save();
    }

    public void InitializePlayerData()
    {
        PlayerData playerData = new PlayerData()
        {
            ColorChoice = 0,
            FaceChoice = 0,
            SoundFxVolume = 1,
            MusicVolume = 1,
            PlayerName = ""
        };
        SetPlayerData(playerData);
        _playerData = playerData;
    }

    public void InitializeRecipeData()
    {
        RecipeData recipeData = new RecipeData()
        {
            RecipeList = new List<RecipeEntry>()
            {
                new RecipeEntry()
                {
                    Unlocked = true,
                    Stars = 0,
                    HighScore = 0,
                    Name = "Fried Egg"
                },
                new RecipeEntry()
                {
                    Unlocked = true,
                    Stars = 0,
                    HighScore = 0,
                    Name = "Veggie Stir Fry"
                },
                new RecipeEntry()
                {
                    Unlocked = false,
                    Stars = 0,
                    HighScore = 0,
                    Name = "Meat!"
                },
                new RecipeEntry()
                {
                    Unlocked = false,
                    Stars = 0,
                    HighScore = 0,
                    Name = "Pasta"
                }
            }
        };

        _recipeData = recipeData;
    }
}
