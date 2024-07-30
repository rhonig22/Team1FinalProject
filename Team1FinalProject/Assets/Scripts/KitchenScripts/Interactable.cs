using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private bool _isInRange;
    [SerializeField] private string _interactKey;
    [SerializeField] private UnityEvent _interactAction;
    void Update()
    {
        if(_isInRange && !KitchenCanvasController.IsRhythmSection)  
        {
            if(Input.GetButtonDown(_interactKey))
            {
                _interactAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _isInRange = true;
        
            Debug.Log("Player now is in range");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _isInRange = false;
        
            Debug.Log("Player now is out of range");
        }
    }
}