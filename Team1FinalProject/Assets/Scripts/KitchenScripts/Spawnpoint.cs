using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bool ifActive = false; 
public class Spawnpoint : MonoBehaviour
{
public GameObject player;
public Transform spawnPos;
public void SpawnPlayer()


    

{
    Destroy(GameObject.FindGameObjectWithTag("Player"));
    Instantiate(player, spawnPos.position, spawnPos.rotation);
}






}