using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] private GameObject _dialogBox;
    [SerializeField] private TextMeshProUGUI _speakerName, _dialogue;
    [SerializeField] private Animator _anim;
    [SerializeField] private Image _speakerSprite;
    [SerializeField] private GameObject _button;

    public UnityEvent DialogueFinished {  get; private set; } = new UnityEvent();
    public bool DialogueOn { get; private set; } = false;

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
        DialogueOn = enable;
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
        EventSystem.current.SetSelectedGameObject(_button);

        ReadNext();
    }

    public void ReadNext()
    {
        if (_typing == null && _currentIndex > _currentConvo.GetLength())
        {
            EnableDialogBox(false);
            DialogueFinished.Invoke();
            DialogueFinished.RemoveAllListeners();
            return;
        }
        else
        {
            if (_typing != null)
            {
                StopCoroutine(_typing);
                _typing = null;
                _dialogue.text = _currentConvo.getLineByIndex(_currentIndex - 1).dialogue;
                return;
            }

            var currentLine = _currentConvo.getLineByIndex(_currentIndex);
            _speakerName.text = currentLine.speaker.GetName();
            var speakerSprite = currentLine.speaker.getSprite();
            _speakerSprite.sprite = speakerSprite;
            _speakerSprite.rectTransform.sizeDelta = new Vector2(speakerSprite.rect.width, speakerSprite.rect.height);
            _typing = StartCoroutine(TypeText(currentLine.dialogue));
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
