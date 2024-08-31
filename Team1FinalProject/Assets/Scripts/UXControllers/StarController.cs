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
    [SerializeField] Animator _animator;
    private int _maxScore;

    private int _currentStarCount = 0;
    private int _currentScore;
    private int _firstStar;
    private int _secondStar;
    private int _quarterStarValue;

    private void Start()
    {
        SetMaxScore(RecipeManager.Instance.GetMaxScore());
    }

    private void Update()
    {
        if (NoteManager.Instance == null)
            return;
        if (NoteManager.Instance.IsBeatStarted)
            SetScore(NoteManager.Instance.Score);
    }

    public void SetScore(int score)
    {
        _currentScore = score;
        SetStars();
    }

    public int GetCurrentStarCount()
    {
        return _currentStarCount;
    }

    public void SetMaxScore(int score)
    {
        _maxScore = score;
        _firstStar = _maxScore / 3;
        _secondStar = 2 * _maxScore / 3;
        _quarterStarValue = _maxScore / 12;
    }

    private void SetStars()
    {
        if (_currentScore >= _maxScore)
        {
            _starImage1.sprite = _fullStar;
            _starImage2.sprite = _fullStar;
            _starImage3.sprite = _fullStar;
            if (_currentStarCount == 2)
                _animator.SetTrigger("ShineStars");
            _currentStarCount = 3;
        }
        else if (_currentScore >= _secondStar)
        {
            _starImage1.sprite = _fullStar;
            _starImage2.sprite = _fullStar;
            _starImage3.sprite = GetPartialStar(_currentScore - _secondStar);
            if (_currentStarCount == 1)
                _animator.SetTrigger("ShineStars");
            _currentStarCount = 2;
        }
        else if (_currentScore >= _firstStar)
        {
            _starImage1.sprite = _fullStar;
            _starImage2.sprite = GetPartialStar(_currentScore - _firstStar);
            _starImage3.sprite = _emptyStar;
            if (_currentStarCount == 0)
                _animator.SetTrigger("ShineStars");
            _currentStarCount = 1;
        }
        else
        {
            _starImage1.sprite = GetPartialStar(_currentScore);
            _starImage2.sprite = _emptyStar;
            _starImage3.sprite = _emptyStar;
            _currentStarCount = 0;
        }
    }

    private Sprite GetPartialStar(int score)
    {
        if (score < _quarterStarValue)
            return _emptyStar;
        if (score < 2 * _quarterStarValue)
            return _quarterStar;
        if (score < 3 * _quarterStarValue)
            return _halfStar;
        
        return _threeQuarterStar;
    }
}
