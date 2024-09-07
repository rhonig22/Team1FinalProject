using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LeaderboardUXController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _nameTexts;
    [SerializeField] private TextMeshProUGUI[] _scoreTexts;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    [SerializeField] private Color32 _highlightColor;
    private int _firstResult = 0;
    private int _lastResult = 10;
    private int _totalResults = 10;
    private int _amount = 10;

    // Start is called before the first frame update
    void Start()
    {
        CallForScores();
    }

    public void CallForScores()
    {
        LeaderboardManager.Instance.GetHighScores(_amount, _firstResult, (results, total) => GenerateLists(results, total));
    }

    public void GenerateLists(LootLockerLeaderboardMember[] results, int total)
    {
        _totalResults = total;
        var playerName = SaveDataManager.Instance.GetPlayerData().PlayerName;
        for (int i = 0; i < _amount; i++)
        {
            if (i < results.Length)
            {
                var result = results[i];
                _nameTexts[i].text = "" + result.rank + ".  " + result.player.name;
                _scoreTexts[i].text = "" + result.score + " Stars!";
                if (result.player.name == playerName)
                {
                    _nameTexts[i].color = _highlightColor;
                    _nameTexts[i].fontStyle = FontStyles.Bold;
                }
            }
            else
            {
                _nameTexts[i].text = "";
                _scoreTexts[i].text = "";
            }
        }

        SetPageButtonStates();
    }

    public void PageLeft()
    {
        if (_firstResult > 0)
        {
            _firstResult -= _amount;
            _lastResult -= _amount;
            CallForScores();
        }
    }

    public void PageRight()
    {
        if (_lastResult < _totalResults)
        {
            _firstResult += _amount;
            _lastResult += _amount;
            CallForScores();
        }
    }

    private void SetPageButtonStates()
    {
        bool isLeftEnabled = _firstResult > 0;
        _leftButton.interactable = isLeftEnabled;
        bool isRightEnabled = _lastResult < _totalResults;
        _rightButton.interactable = isRightEnabled;
    }
}
