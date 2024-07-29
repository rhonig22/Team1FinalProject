using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private GameObject _dialogBox;
    [SerializeField] private TextMeshProUGUI _speakerName, _dialogue, _navButtonText;
    [SerializeField] private Animator _anim;
    [SerializeField] private Image _speakerSprite;

    public UnityEvent DialogueFinished {  get; private set; } = new UnityEvent();

    private int _currentIndex;
    private Conversation _currentConvo;
    private Coroutine _typing;

    private void Awake()
    {
        if (Instance == null)
        {
            //make the dialogue manager this one
            Instance = this;
        }
        else
        {
            //only one at a time is allowed
            Destroy(gameObject);
        }
    }

    private void EnableDialogBox(bool enable)
    {
        _dialogBox.SetActive(enable);
        _anim.SetBool("isOpen", enable);
    }

    public void StartConversation(Conversation convo)
    {
        EnableDialogBox(true);
        _currentIndex = 0;
        _currentConvo = convo;
        _speakerName.text = "";
        _dialogue.text = "";
        _navButtonText.text = ">";

        ReadNext();
    }

    public void ReadNext()
    {
        if (_currentIndex >= _currentConvo.GetLength())
        {
            //Debug.Log("currentIndex: " + currentIndex + " and currentConvo: " + currentConvo.GetLength());
            _navButtonText.text = "X";
        }
        if (_currentIndex > _currentConvo.GetLength())
        {
            EnableDialogBox(false);
            DialogueFinished.Invoke();
            DialogueFinished.RemoveAllListeners();
            return;
        }
        else
        {
            _speakerName.text = _currentConvo.getLineByIndex(_currentIndex).speaker.GetName();

            if (_typing == null)
            {
                _typing = StartCoroutine(TypeText(_currentConvo.getLineByIndex(_currentIndex).dialogue));
            }
            else
            {
                StopCoroutine(_typing);
                _typing = null;
                _typing = StartCoroutine(TypeText(_currentConvo.getLineByIndex(_currentIndex).dialogue));

            }
            //dialogue.text = currentConvo.getLineByIndex(currentIndex).dialogue;
            _speakerSprite.sprite = _currentConvo.getLineByIndex(_currentIndex).speaker.getSprite();
            _currentIndex++;
        }
       
    }

    private IEnumerator TypeText(string text)
    {
        _dialogue.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            _dialogue.text += text[i];
            yield return new WaitForSeconds(0.05f);
        }

        _typing = null;
    }

}
