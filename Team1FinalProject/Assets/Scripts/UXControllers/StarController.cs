using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class StarController : MonoBehaviour
{
    [SerializeField] private Image _starImage1;
    [SerializeField] private Image _starImage2;
    [SerializeField] private Image _starImage3;
    [SerializeField] Sprite _emptyStar;
    [SerializeField] Sprite _quarterStar;
    [SerializeField] Sprite _halfStar;
    [SerializeField] Sprite _threeQuarterStar;
    [SerializeField] Sprite _fullStar;
    [SerializeField] private int _maxScore;

    private int _currentScore;
    private int _firstStar;
    private int _secondStar;
    private int _quarterStarValue;

    private void Start()
    {
        _firstStar = _maxScore / 3;
        _secondStar = 2 * _maxScore / 3;
        _quarterStarValue = _maxScore / 12;
    }

    private void Update()
    {
        int _currentScore = NoteManager.Instance.Score;
        if (_currentScore >= _maxScore)
        {
            _starImage3.sprite = _fullStar;
        }
        else if (_currentScore >= _secondStar)
        {
            _starImage2.sprite = _fullStar;
            _starImage3.sprite = GetPartialStar(_currentScore - _secondStar);
        }
        else if (_currentScore >= _firstStar)
        {
            _starImage1.sprite = _fullStar;
            _starImage2.sprite = GetPartialStar(_currentScore - _firstStar);
        }
        else
        {
            _starImage1.sprite = GetPartialStar(_currentScore);
        }
    }

    private Sprite GetPartialStar(int score)
    {
        if (score < _quarterStarValue)
            return _emptyStar;
        if (score < 2 * _quarterStarValue)
            return _halfStar;
        if (score < 3 * _quarterStarValue)
            return _threeQuarterStar;
        
        return _quarterStar;
    }
}
