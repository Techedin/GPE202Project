using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardsKey;
    public KeyCode turnLeftKey;
    public KeyCode turnRightKey;

    // Start is called before the first frame update
    public override void Start()
    {
        // Run the Start() function from the parent (base) class
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        ProcessInputs();

        base.Update();
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
    }
}
