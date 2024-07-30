using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bool ifActive = false; 
public class Spawnpoint : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    public void SpawnPlayer()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Instantiate(_playerPrefab, transform.position, transform.rotation);
    }
}