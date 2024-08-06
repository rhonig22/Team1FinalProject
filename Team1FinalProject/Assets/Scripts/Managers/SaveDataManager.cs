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

    public RecipeData GetRecipeData()
    {
        return _recipeData;
    }

    public void SetRecipeData(RecipeData recipeData)
    {
        PlayerPrefs.SetString(_recipeDataKey, JsonUtility.ToJson(recipeData));
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
    }

    public void InitializeRecipeData()
    {
        RecipeData recipeData = new RecipeData()
        {
            RecipeList = new RecipeEntry[]
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
    }
}
