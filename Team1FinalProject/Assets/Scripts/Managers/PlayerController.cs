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

        if (Input.GetAxis("DPad Y") > 0.1f)
        {
            Debug.Log("Y greater than 1");
            _stoveTopSpawn.SpawnPlayer(_player);
        }
        else if (Input.GetAxis("DPad X") > 0.1f)
        {
            Debug.Log("X greater than 1");
            _fridgeSpawn.SpawnPlayer(_player);
        }
        else if (Input.GetAxis("DPad Y") < -0.1f)
        {
            Debug.Log("Y less than 1");
            _prepSpawn.SpawnPlayer(_player);
        }
        else if (Input.GetAxis("DPad X") < -0.1f)
        {
            Debug.Log("X less than 1");
            _flattopSpawn.SpawnPlayer(_player);
        }

    }
}
