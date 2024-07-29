using mixpanel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MixpanelLogger : MonoBehaviour
{
    public static MixpanelLogger Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Mixpanel.StartTimedEvent("Session Ended");
    }

    public void LogEvent(string eventName, Value properties)
    {
        Mixpanel.Track(eventName, properties);
    }

    private void OnApplicationQuit()
    {
        Mixpanel.Track("Session Ended");
    }
}
