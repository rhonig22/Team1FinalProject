using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    void Start()
    {
        
    }

    
    void Update()
    {
      if(isInRange)  
      {
        if(Input.GetKeyDown(interactKey))
        {
            interactAction.Invoke();
        }
      }
    }

private void OnTriggerEnter2D(Collider2D collision)
{
    if(collision.gameObject.CompareTag("Player"))
    {
        isInRange = true;
        
        Debug.Log("Player now is in range");
    }
}

private void OnTriggerExit2D(Collider2D collision)
{
if(collision.gameObject.CompareTag("Player"))
    {
        isInRange = false;
        
        Debug.Log("Player now is out of range");
    }
}

}



