using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Spawnpoint _stoveTopSpawn;
    [SerializeField] Spawnpoint _fridgeSpawn;
    [SerializeField] Spawnpoint _prepSpawn;
    [SerializeField] Spawnpoint _flattopSpawn;
    [SerializeField] GameObject _player;
    
    void Update()
    {
        if (!KitchenCanvasController.IsRhythmSection)
        {
           

            if (Input.GetAxisRaw("DPad Y") > 0.1f)
            {
                Debug.Log("Y greater than 1");
                transform.position = _stoveTopSpawn.transform.position;

            }
            else if (Input.GetAxisRaw("DPad X") > 0.1f)
            {
                Debug.Log("X greater than 1");
                transform.position = _fridgeSpawn.transform.position;

            }
            else if (Input.GetAxisRaw("DPad Y") < -0.1f)
            {
                Debug.Log("Y less than 1");
                transform.position = _prepSpawn.transform.position;
            }
            else if (Input.GetAxisRaw("DPad X") < -0.1f)
            {
                Debug.Log("X less than 1");
                transform.position = _flattopSpawn.transform.position;
            }
        }
    }
}
