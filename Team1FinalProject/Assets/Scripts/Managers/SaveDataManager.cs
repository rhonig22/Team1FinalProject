using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager Instance;
    private readonly string _playerDataKey = "PlayerData";
    private readonly string _recipeDataKey = "RecipeData";
    private readonly string _unlockablesKey = "UnlockablesData";
    private PlayerData _playerData;
    private RecipeData _recipeData;
    private UnlockablesData _unlockables;
    private Dictionary<string, bool> _unlockableMap = new Dictionary<string, bool>();

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
            var playerDataString = PlayerPrefs.GetString(_playerDataKey);
            _playerData = JsonUtility.FromJson<PlayerData>(playerDataString);
            Debug.Log("playerData: " + playerDataString);
        }
        else
        {
            InitializePlayerData();
        }

        if (PlayerPrefs.HasKey(_recipeDataKey))
        {
            var recipeDataString = PlayerPrefs.GetString(_recipeDataKey);
            _recipeData = JsonUtility.FromJson<RecipeData>(recipeDataString);
            Debug.Log("recipeData: " + recipeDataString);
        }
        else
        {
            InitializeRecipeData();
        }

        if (PlayerPrefs.HasKey(_unlockablesKey))
        {
            var unlockablesString = PlayerPrefs.GetString(_unlockablesKey);
            _unlockables = JsonUtility.FromJson<UnlockablesData>(unlockablesString);
            Debug.Log("unlockables: " + unlockablesString);
        }
        else
        {
            InitializeUnlockablesData();
        }

        SetupUnlockableMap();
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

    public int GetStarCount()
    {
        int stars = 0;
        foreach (var recipe in _recipeData.RecipeList)
        {
            stars += recipe.Stars;
        }

        return stars;
    }

    public int CountPlayedRecipes()
    {
        int count = 0;
        foreach (var recipe in _recipeData.RecipeList)
        {
            if (recipe.HighScore > 0)
                count++;
        }

        return count;
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
            PlayerName = "chef" + Random.Range(1000, 10000),
            Prep = Aesthetic.Modest,
            Flattop = Aesthetic.Modest,
            Stovetop = Aesthetic.Modest,
            Fridge = Aesthetic.Modest,
            Floor = Aesthetic.Modest,
            Wall = Aesthetic.Modest,
        };
        SetPlayerData(playerData);
        _playerData = playerData;
    }

    public void SetPlayerName(string name)
    {
        _playerData.PlayerName = name;
        SetPlayerData(_playerData);
    }

    public void SetPlayerId(string id)
    {
        _playerData.PlayerId = id;
        SetPlayerData(_playerData);
    }

    public void InitializeRecipeData()
    {
        RecipeData recipeData = new RecipeData()
        {
            RecipeList = new List<RecipeEntry>()
        };

        _recipeData = recipeData;
    }

    public RecipeEntry InitializeRecipeEntry(string name, bool unlocked)
    {
        var entry = new RecipeEntry()
        {
            Unlocked = unlocked,
            Stars = 0,
            HighScore = 0,
            Name = name
        };

        _recipeData.RecipeList.Add(entry);
        return entry;
    }

    public void SetUnlockablesData()
    {
        PlayerPrefs.SetString(_unlockablesKey, JsonUtility.ToJson(_unlockables));
        PlayerPrefs.Save();
    }

    public void InitializeUnlockablesData()
    {
        UnlockablesData unlockablesData = new UnlockablesData()
        {
            UnlockablesList = new List<Unlockable>()
        };

        _unlockables = unlockablesData;
    }

    public void UnlockedSomething(string unlockable)
    {
        var unlocked = new Unlockable()
        {
            Name = unlockable,
            Unlocked = true,
            UnlockedTime = Time.time
        };

        _unlockables.UnlockablesList.Add(unlocked);
        _unlockableMap[unlockable] = true;
        SetUnlockablesData();
    }

    public bool IsUnlocked(string unlockable)
    {
        if (_unlockableMap.ContainsKey(unlockable))
            return _unlockableMap[unlockable];

        return false;
    }

    private void SetupUnlockableMap()
    {
        foreach (var unlockable in _unlockables.UnlockablesList)
        {
            _unlockableMap[unlockable.Name] = true;
        }
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        _playerData = null;
        _unlockables = null;
        _recipeData = null;
        _unlockableMap = new Dictionary<string, bool>();
        SetUpDataManager();
        CustomizationManager.Instance.ResetCustomizations();
    }
}
