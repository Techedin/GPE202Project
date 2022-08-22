using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Controller
{
    //Changable public keycodes so people can do what they want with the controls. Will probbly come in handy for a settings menu
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardsKey;
    public KeyCode turnLeftKey;
    public KeyCode turnRightKey;
    public KeyCode jumpKey;
    public KeyCode shootKey;
    public KeyCode sneakKey;

    public Text playerStats;

    public Camera playerCamera;
   

    // Start is called before the first frame update
    public override void Start()
    {
        playerCamera = pawn.gameObject.GetComponentInChildren<Camera>();
        //check for gameManager
        if (GameManager.gameManagerInstance != null)
        {
            //Check to see if this is in the list
            if(GameManager.gameManagerInstance.players != null)
            {
                //add to active players list
                GameManager.gameManagerInstance.players.Add(this);
               
            }
            if(GameManager.gameManagerInstance.playerCameras != null)
            {
                GameManager.gameManagerInstance.playerCameras.Add(playerCamera);
            }
        }
      

        playerStats = pawn.gameObject.GetComponentInChildren<Text>();
        UpdateText();

        
        // Run the Start() function from the parent (base) class
        base.Start();
    }


    // Update is called once per frame
    public override void Update()
    {
      
        //Read my input everyframe
        ProcessInputs();
        
        base.Update();
    }

    public void OnDestroy()
    {
        if (GameManager.gameManagerInstance != null)
        {
            //Check to see if this is in the list
            if (GameManager.gameManagerInstance.players != null)
            {
                //remove from active players list
                GameManager.gameManagerInstance.players.Remove(this);
            }
        }
    }

    public override void ProcessInputs()
    {
        if (Input.GetKey(moveForwardKey))
        {

            pawn.MoveForward();
        }
        if (Input.GetKey(moveBackwardsKey))
        {
            pawn.MoveBackwards();
        }
        if (Input.GetKey(turnLeftKey))
        {
            pawn.MoveLeft();
        }
        if (Input.GetKey(turnRightKey))
        {
            pawn.MoveRight();
        }
        if (Input.GetKey(jumpKey))
        {
            //Gotta get up
            pawn.Jump();
        }
        if (Input.GetKey(shootKey))
        {
            pawn.Shoot();
        }
        if (Input.GetKey(sneakKey))
        {
            pawn.Sneak();
        }
        if (Input.GetKeyUp(sneakKey))
        {
            pawn.isSneaking = false;
        }

    }

    public override void OnDied()
    {
        base.OnDied();
        if(livesTotal <= 0)
        {
            GameManager.gameManagerInstance.ActivateGameOver();
        }
        else
        {
            pawn.health.currentHealth = pawn.health.maxHealth;
            pawn.controller.livesTotal -= 1;
            UpdateText();
            GameManager.gameManagerInstance.RespawnPlayer(pawn.controller);
        }

    }

    public override void AddPoints(int pointToAdd)
    {
        UpdateText();
        pointTotal += pointToAdd;

    }

    public void UpdateText()
    {
        playerStats.text = "Player Lives: " + livesTotal + " Player Points: " + pointTotal;
    }

}
