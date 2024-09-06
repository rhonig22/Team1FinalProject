using LootLocker.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }
    public static bool WasDataRetrieved { get; private set; } = false;
    public static UnityEvent UserDataRetrieved = new UnityEvent();
    private readonly string _leaderboardID = "starchef_leaderboard";

    // Start is called before the first frame update
    void Start()
    {
        // Set up singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        UserDataRetrieved.AddListener(() => { WasDataRetrieved = true; });
        StartLootLockerSession();
    }

    private void StartLootLockerSession()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");
                return;
            }

            var playerId = response.player_identifier;
            SaveDataManager.Instance.SetPlayerId(playerId);
            LootLockerSDKManager.GetPlayerName((response) =>
            {
                if (!response.success)
                {
                    return;
                }

                if (string.IsNullOrEmpty(response.name))
                    SetUserName(SaveDataManager.Instance.GetPlayerData().PlayerName, (name) => { });
                else
                    SaveDataManager.Instance.SetPlayerName(response.name);
                
                UserDataRetrieved.Invoke();
                Debug.Log("successfully started LootLocker session");
            });
        });
    }

    public void SubmitLootLockerScore(int score)
    {
        var playerData = SaveDataManager.Instance.GetPlayerData();
        LootLockerSDKManager.SubmitScore(playerData.PlayerId, score, _leaderboardID, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful");
            }
        });
    }

    public void SetUserName(string username, Action<string> callback)
    {
        LootLockerSDKManager.SetPlayerName(username, (response) =>
        {
            if (!response.success)
            {
                callback(null);
                return;
            }

            SaveDataManager.Instance.SetPlayerName(response.name);
            callback(response.name);
            Debug.Log("successfully set User name to " + response.name);
        });
    }

    public void GetHighScores(int count, int start, Action<LootLockerLeaderboardMember[], int> callback)
    {
        UnityAction getScores = () =>
        {
            LootLockerSDKManager.GetScoreList(_leaderboardID, count, start, (response) =>
            {
                if (response.statusCode == 200)
                {
                    Debug.Log("Successful");
                    callback(response.items, response.pagination.total);
                }
            });
        };

        if (WasDataRetrieved)
            getScores();
        else
            UserDataRetrieved.AddListener(getScores);
    }
}
