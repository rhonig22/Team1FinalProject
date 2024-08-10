using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStepAppliance : MonoBehaviour
{
    [SerializeField] private Station _station;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(RecipeManager.Instance.GetNextStep().Station == _station)
        {
            Debug.Log("Station:  "+ _station);
        }
      
    }
}
