using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List <PlayerController> players;
    public Transform playerSpawn;
    public static GameManager gameManagerInstance;
    public void Awake()
    {
        //Is there a boss
        if (gameManagerInstance == null)
        {
            //I'm the boss
            gameManagerInstance = this;
            //I'm the boss even in another place
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            //there's already a boss. There can be only one
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        SpawnPlayer();
    }

    //Prefabs
    public GameObject playerControlPrefab;
    public GameObject tankPrefab;


    private void SpawnPlayer()
    {
        //Spawns a new PlayerController Holder at root scene positions and assigns it to a varible so we have a refernce to it.
        GameObject newPlayerControllerHolder = Instantiate(playerControlPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        //Spawns the tank prefab and save it to a varible
        GameObject newPlayerPawn = Instantiate(tankPrefab, playerSpawn.position, playerSpawn.rotation) as GameObject;
        //Grab the PC from the PC holder
        Controller newPlayerController = newPlayerControllerHolder.GetComponent<Controller>();
        //Grab the tank pawn
        Pawn newPawn = newPlayerPawn.GetComponent<Pawn>();
        //assign it so we can control the tank pawn
        newPlayerController.pawn = newPawn;
    }


}
