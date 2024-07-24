using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberGenerator : MonoBehaviour
{
    public bool interactAction;

    public void generateNumber()
    {
        if(!interactAction)
        {
            Debug.Log(Random.Range(0, 100));
        }
    }


  
}
