using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class DemoStartAnimationController : MonoBehaviour
{
    [SerializeField] private GameObject _three;
    [SerializeField] private GameObject _two;
    [SerializeField] private GameObject _one;
    [SerializeField] private Animator _startAnimator;
    private int _animationCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartAnimation();
    }

    public void StartAnimation()
    {
        switch (_animationCount)
        {
            case 0:
                _three.SetActive(true);
                _startAnimator.SetTrigger("Start");
                break;

            case 1:
                _three.SetActive(false);
                _two.SetActive(true);
                _startAnimator.SetTrigger("Start");
                break;
            case 2:
                _two.SetActive(false);
                _one.SetActive(true);
                _startAnimator.SetTrigger("Start");
                break;
            case 3:
                _one.SetActive(false);
                NoteManager.Instance.StartBeats();
                break;
            default:
                break;
        }

        _animationCount++;
    }
}
