using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    private SphereCollider groundTest;
    private bool isTouching;
    public Transform shootPoint;

    // Start is called before the first frame update
    public override void Start()
    {

        // Run the Start() function from the parent (base) class
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Run the Start() function from the parent (base) class
        base.Start();



    }

    public override void MoveForward()
    {
        if (mover != null)
        {
            mover.Move(transform.forward, moveSpeed);
        }
        else
        {
            Debug.LogWarning("Warning: No Mover in TankPawn.MoveForward()!");
        }

    }
    public override void MoveBackwards()
    {
        if (mover != null)
        {
            mover.Move(transform.forward, -moveSpeed);
        }
        else
        {
            Debug.LogWarning("Warning: No Mover in TankPawn.MoveForward()!");
        }
    }
    public override void MoveRight()
    {
        if (mover != null)
        {
            mover.Rotate(turnSpeed);
        }
        else
        {
            Debug.LogWarning("Warning: No Mover in TankPawn.MoveForward()!");
        }

    }
    public override void MoveLeft()
    {
        if (mover != null)
        {
            mover.Rotate(-turnSpeed);
        }
        else
        {
            Debug.LogWarning("Warning: No Mover in TankPawn.MoveForward()!");
        }
    }

    public override void Jump()
    {
        if (mover != null)
        {
            
                //get the green(up) arrow so I can ACEND
                mover.Move(transform.up, moveSpeed);

        }
        else
        {
            Debug.LogWarning("Warning: No Mover in TankPawn.MoveForward()!");
        }
    }

}
