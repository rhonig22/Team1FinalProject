using mixpanel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class MixpanelLogger : MonoBehaviour
{
    public static MixpanelLogger Instance;
    private bool IsMixpanelTurnedOn = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        StartCoroutine(SendMixPanelRequest("https://api.mixpanel.com/track/?ip=1"));
    }

    public void LogEvent(string eventName, Value properties)
    {
        if (!IsMixpanelTurnedOn)
            return;

        Mixpanel.Track(eventName, properties);
    }

    private void OnApplicationQuit()
    {
        if (!IsMixpanelTurnedOn)
            return;

        Mixpanel.Track("Session Ended");
    }

    public IEnumerator SendMixPanelRequest(string url)
    {
        UnityWebRequest request;
        request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Mixpanel Request succeeded: " + request.downloadHandler.text);
            Mixpanel.StartTimedEvent("Session Ended");
            IsMixpanelTurnedOn = true;
            yield break; // Exit if successful
        }
        else
        {
            Debug.LogError($"Mixpanel Request failed: {request.error}");
            IsMixpanelTurnedOn = false;
        }
    }
}
