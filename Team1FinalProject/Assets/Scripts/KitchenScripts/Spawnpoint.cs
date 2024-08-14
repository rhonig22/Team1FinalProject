using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bool ifActive = false; 
public class Spawnpoint : MonoBehaviour
{
    //[SerializeField] private GameObject _playerPrefab;
    public void SpawnPlayer(GameObject _player)
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Instantiate(_player, transform.position, transform.rotation);
    }
}