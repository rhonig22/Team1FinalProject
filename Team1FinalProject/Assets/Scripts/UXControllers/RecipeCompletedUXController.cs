using mixpanel;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeCompletedUXController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _recipeText;
    [SerializeField] private TextMeshProUGUI _recipeMessage;
    [SerializeField] private StarController _starController;
    [SerializeField] private CanvasRenderer _victoryFoodRenderer;
    [SerializeField] private Button _completedButton;
    [SerializeField] private GameObject _victoryParticles;
    [SerializeField] private AudioClip _victorySound;
    private List<ParticleSystem> _particleSystems = new List<ParticleSystem>();
    private readonly float _waitBetweenParticles = .1f;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_completedButton.gameObject);
        var recipeName = RecipeManager.Instance.GetRecipeName();
       
        _recipeText.text = recipeName;
      
        var recipeEntry = SaveDataManager.Instance.GetRecipeEntry(recipeName);
        //not yet working dynamically?
        _victoryFoodRenderer.GetComponent<Image>().sprite = RecipeManager.Instance.GetRecipeVictorySprite();
        _starController.SetMaxScore(RecipeManager.Instance.GetMaxScore());
        _starController.SetScore(NoteManager.Instance.Score);
        var starCount = _starController.GetCurrentStarCount();
        _recipeMessage.text = RecipeManager.Instance.GetRecipeMessage(starCount);

        if (recipeEntry == null )
        {
            recipeEntry = new RecipeEntry();
            recipeEntry.Name = recipeName;
            recipeEntry.HighScore = 0;
        }

        if (NoteManager.Instance.Score > recipeEntry.HighScore)
        {
            if (recipeEntry.HighScore > 0)
                RecipeManager.Instance.SetImprovedMessage();

            recipeEntry.Stars = starCount;
            recipeEntry.HighScore = NoteManager.Instance.Score;
            SaveDataManager.Instance.SetRecipeEntryData(recipeEntry);
            LeaderboardManager.Instance.SubmitLootLockerScore(SaveDataManager.Instance.GetStarCount());
        }
        else
        {
            RecipeManager.Instance.SetNotAsGoodMessage();
        }

        var props = new Value();
        props["recipeName"] = recipeName;
        props["score"] = NoteManager.Instance.Score;
        MixpanelLogger.Instance.LogEvent("Recipe Completed", props);

        NoteManager.Instance.BeatEvent.AddListener((int beat) => { ParticleBurst(starCount, beat); });
        if (starCount > 2)
            SoundManager.Instance.PlaySound(_victorySound, transform.position);
    }

    private void ParticleBurst(int count, int beat)
    {
        if (beat % 4 != 0)
            return;

        if (_particleSystems.Count == 0)
        {
            for (int i = 0; i < count; i++)
            {
                var particle = Instantiate(_victoryParticles, transform.parent);
                _particleSystems.Add(particle.GetComponent<ParticleSystem>());
            }
        }

        StartCoroutine(PlayParticles());
    }

    IEnumerator PlayParticles()
    {
        foreach (var particleSystem in _particleSystems)
        {
            particleSystem.transform.localPosition = new Vector3(Random.Range(-500, 501), Random.Range(-300, 300), 0);
            particleSystem.Play();
            yield return new WaitForSeconds(_waitBetweenParticles);
        }
    }
}
