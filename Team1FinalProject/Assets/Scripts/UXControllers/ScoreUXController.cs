using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUXController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Update()
    {
        _scoreText.text = "Score: " + NoteManager.Instance.Score.ToString();
    }
}
