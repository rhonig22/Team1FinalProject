using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationButtonPulser : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }
    //we
    public void Update()
    {
            

        RecipeStep step = RecipeManager.Instance.GetNextStep();
        if (step.Station == Station.FlatTop)
        {
            transform.localPosition = new Vector3(-99, 210, 0);
        }
        else if (step.Station == Station.Stovetop)
        {
            transform.localPosition = new Vector3(296, 228, 0);
        }
        else if (step.Station == Station.Fridge)
        {
            transform.localPosition = new Vector3(650, 175, 0);
        }
        else
        {
            transform.localPosition = new Vector3(227, -224, 0);
        }
    }
    // Update is called once per frame
  
}
