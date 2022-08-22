using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<PlayerController> players;
    public List<AIController> AI;
    public List<Camera> playerCameras;
    public GameObject playerSpawnObject;

    public MapGenerator mapGenerator;

    public Transform playerSpawn;
    public static GameManager gameManagerInstance;

    // Game States
    public GameObject TitleScreenStateOb;
    public GameObject MainMenuStateOb;
    public GameObject OptionsScreenStateOb;
    public GameObject CreditsScreenStateOb;
    public GameObject GameplayStateOb;
    public GameObject GameOverScreenStateOb;

    //Prefabs
    public GameObject playerControlPrefab;
    public GameObject tankPrefab;

    //Audio
    public AudioSource mainMenuMusic;
    public AudioSource gameMusic;

    public Rect player1;
    public Rect player2;

    public bool isSplitScreen;
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
        ActivateTitleScreen();
        ActivateTitleScreen();
        //Eveything below needs to be done to start the game

    }
    public void Update()
    {
       
    }
    public void LateUpdate()
    {
        if(isSplitScreen == true)
        {
            SplitScreenCamera();
        }
    }

    private void SpawnPlayer()
    {

        //Spawns the tank prefab and save it to a varible
        GameObject newPlayerPawnHolder = Instantiate(tankPrefab, playerSpawn.position, playerSpawn.rotation) as GameObject;
        //Grab the PC from the PC holder

        //Grab the tank pawn
        Pawn newPawn = newPlayerPawnHolder.GetComponent<Pawn>();
        //assign it so we can control the tank pawn

        //Spawns a new PlayerController Holder at root scene positions and assigns it to a varible so we have a refernce to it.

        GameObject newPlayerControllerHolder = Instantiate(playerControlPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        Controller newPlayerController = newPlayerControllerHolder.GetComponent<Controller>();
        newPlayerController.pawn = newPawn;
        newPawn.controller = newPlayerController;

    }
    private void SpawnSplitScreen()
    {
        for(int i=0; i < 2; i++)
        {
            GameObject newPlayerPawnHolder = Instantiate(tankPrefab, playerSpawn.position, playerSpawn.rotation) as GameObject;
            //Grab the PC from the PC holder
            //Grab the tank pawn
            Pawn newPawn = newPlayerPawnHolder.GetComponent<Pawn>();
            //Spawns a new PlayerController Holder at root scene positions and assigns it to a varible so we have a refernce to it.
            GameObject newPlayerControllerHolder = Instantiate(playerControlPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            Controller newPlayerController = newPlayerControllerHolder.GetComponent<Controller>();
            //assign it so we can control the tank pawn
            newPlayerController.pawn = newPawn;
            newPawn.controller = newPlayerController;
            
      
        }
     
    }
    public void SplitScreenCamera()
    {
     
            Debug.Log("Player Camera Changed");
            player1 = new Rect(0, 0, (float)0.5, 1);
            player2 = new Rect((float)0.5, 0, (float)0.5, 1);
        playerCameras[0].rect = player1;
        playerCameras[1].rect = player2;

    }


    public void RespawnPlayer(Controller playertoRespawn)
    {
        GameObject newPlayerPawnHolder = Instantiate(tankPrefab, playerSpawn.position, playerSpawn.rotation) as GameObject;
        Pawn newPawn = newPlayerPawnHolder.GetComponent<Pawn>();
        playertoRespawn.pawn = newPawn;
        playertoRespawn.pawn.controller = playertoRespawn;
        playertoRespawn.Start();
        foreach(AIController aIController in AI)
        {
            
            aIController.target = playertoRespawn.pawn.gameObject;
            aIController.lastHeardLocation = aIController.target.transform;

        }
    }

    private void DespawnPlayer(PlayerController playerController, GameObject playerGameobject)
    {

        Destroy(playerGameobject);

    }

    private void DeactivateAllStates()
    {
        // Deactivate all Game States
        TitleScreenStateOb.SetActive(false);
        MainMenuStateOb.SetActive(false);
        OptionsScreenStateOb.SetActive(false);
        CreditsScreenStateOb.SetActive(false);
        GameplayStateOb.SetActive(false);
        GameOverScreenStateOb.SetActive(false);
    }

    public void ActivateTitleScreen()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the title screen
        TitleScreenStateOb.SetActive(true);
        //ToDO: Make shit do things to change states
        mainMenuMusic.Play();
    }
    public void ActivateMainMenuScreen()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the title screen
        MainMenuStateOb.SetActive(true);
        //ToDO: Make shit do things to change states
        if (Input.GetKey(KeyCode.Alpha2))
        {
            ActivateOptionsScreen();
        }
    }
    public void ActivateOptionsScreen()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the title screen
        OptionsScreenStateOb.SetActive(true);
        //ToDO: Make shit do things to change states
        if (Input.GetKey(KeyCode.Alpha3))
        {
            ActivateCreditsScreen();

        }
    }
    public void ActivateCreditsScreen()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the title screen
        CreditsScreenStateOb.SetActive(true);
        //ToDO: Make shit do things to change states
        if (Input.GetKey(KeyCode.Alpha4))
        {
            ActivateGameplay();

        }
    }
    public void ActivateGameplay()
    {
        // Deactivate all states
        DeactivateAllStates();
        mainMenuMusic.Stop();


        gameMusic.Play();
        // Activate the title screen
        GameplayStateOb.SetActive(true);
        //ToDO: Make shit do things to change states
        mapGenerator.GenerateMap();

        if(isSplitScreen != true)
        {
            PlayerSpawnPoint[] playerSpawns = FindObjectsOfType<PlayerSpawnPoint>();

            playerSpawn = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;

            SpawnPlayer();
        }
        else
        {
            PlayerSpawnPoint[] playerSpawns = FindObjectsOfType<PlayerSpawnPoint>();

            playerSpawn = playerSpawns[Random.Range(0, playerSpawns.Length)].transform;

            SpawnSplitScreen();
          
        }

       

    }
    public void ActivateGameOver()
    {
        // Deactivate all states
        DeactivateAllStates();
        // Activate the title screen
        GameOverScreenStateOb.SetActive(true);

        mapGenerator.DeleteMap();

        foreach (PlayerController playerCont in players)
        {
            DespawnPlayer(playerCont, playerCont.pawn.gameObject);
        }
        players.Clear();


        //ToDO: Make shit do things to change states
        if (Input.GetKey(KeyCode.Alpha6))
        {
            ActivateGameplay();

        }

    }

    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void SplitScreenBool()
    {
        if (isSplitScreen == true)
        {
            isSplitScreen = false;
        }
        else
        {
            isSplitScreen = true;
        }
    }
    IEnumerator annoying()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);
    }
}
