using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private AudioClip _backingTrack;
    public static bool IsUnlockedMode { get; private set; } = false;
    private readonly string _titleScene = "TitleScene";
    private readonly string _kitchenScene = "KitchenScene";
    private readonly string _settingsScene = "SettingsScene";
    private readonly string _demoScene = "DemoQTE";
    private readonly string _introStoryScene = "IntroStoryScene";
    private readonly string _recipeBookScene = "RecipeBookScene";
    private readonly string _controlsScene = "ControlsScene";
    private readonly string _creditsScene = "CreditsScene";
    private readonly string _hubScene = "HubScene";
    private List<string> _backStack = new List<string>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayBackingTrack();
    }

    // called second
    private void OnLevelWasLoaded(int level)
    {
        TransitionManager.Instance.FadeIn(() => { });
    }

    private void Update()
    {
        if (Input.GetButtonDown("Enable Debug Button 1"))
        {
            IsUnlockedMode = true;
        }
    }

    public void LoadDemo()
    {
        AddToBackstack(SceneManager.GetActiveScene().name);
        LoadScene(_demoScene);
    }

    public void LoadSettings()
    {
        AddToBackstack(SceneManager.GetActiveScene().name);
        LoadScene(_settingsScene);
    }

    public void LoadKitchen()
    {
        AddToBackstack(SceneManager.GetActiveScene().name);
        LoadScene(_kitchenScene);
    }

    public void LoadIntro()
    {
        AddToBackstack(SceneManager.GetActiveScene().name);
        LoadScene(_introStoryScene);
    }

    public void LoadRecipeBook(bool addToBackStack = true)
    {
        if (addToBackStack)
            AddToBackstack(SceneManager.GetActiveScene().name);
        LoadScene(_recipeBookScene);
    }

    public void LoadHubScene()
    {
        AddToBackstack(SceneManager.GetActiveScene().name);
        LoadScene(_hubScene);
    }

    public void LoadTitleScreen()
    {
        _backStack.Clear();
        LoadScene(_titleScene);
    }

    public void LoadControls()
    {
        AddToBackstack(SceneManager.GetActiveScene().name);
        LoadScene(_controlsScene);
    }

    public void LoadCredits()
    {
        AddToBackstack(SceneManager.GetActiveScene().name);
        LoadScene(_creditsScene);
    }

    public void PopBack()
    {
        var last = _backStack.Count - 1;
        _backStack.RemoveAt(last);
    }

    public void GoBack()
    {
        if (_backStack.Count > 0)
        {
            var last = _backStack.Count - 1;
            var back = _backStack[last];
            _backStack.RemoveAt(last);
            LoadScene(back);
        }
        else
        {
            LoadScene(_titleScene);
        }
    }
    private void LoadScene(string sceneName)
    {
        UnityAction loadNextBoss = () => { SceneManager.LoadScene(sceneName); };
        TransitionManager.Instance.FadeOut(loadNextBoss);
    }

    private void AddToBackstack(string sceneName)
    {
        if (sceneName == _introStoryScene)
            return;

        _backStack.Add(sceneName);
    }

    public void PlayBackingTrack()
    {
        var music = MusicManager.Instance.getMusicSource();
        if (music.isPlaying && music.clip == _backingTrack)
            return;

        MusicManager.Instance.PlayMusicClip(_backingTrack);
    }
}