using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    private SphereCollider groundTest;
    private bool canShoot;
    [SerializeField] private float shootCooldown;

   

    // Start is called before the first frame update
    public override void Start()
    {

        // Run the Start() function from the parent (base) class
        base.Start();
        shootCooldown = firerate;
        canShoot = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        // Run the Update() function from the parent (base) class
        base.Update();
        
        if (shootCooldown <= 0)
        {
            canShoot = true;

        }
        else
        {
            shootCooldown -= Time.deltaTime;
        }

    }

    public override void MoveForward()
    {
        if (mover != null)
        {
            
            if(isSneaking != true)
            {
                noise.volumeDistance = 3;
                mover.Move(transform.forward, moveSpeed);
            }
            else if(isSneaking == true)
            {
                mover.Move(transform.forward, sneakSpeed);
            }
            
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
            if (isSneaking != true)
            {
                noise.volumeDistance = 3;
                mover.Move(transform.forward, -moveSpeed);
            }
            else if (isSneaking == true)
            {
                mover.Move(transform.forward, -sneakSpeed);
            }

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
            if (isSneaking != true)
            {
                noise.volumeDistance = 3;
                mover.Rotate(turnSpeed);
            }
            else if(isSneaking == true)
            {
                mover.Rotate(sneakTurnSpeed);
            }
                
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
            if (isSneaking != true)
            {
                noise.volumeDistance = 3;
                mover.Rotate(-turnSpeed);
            }
            else if(isSneaking == true)
            {
                mover.Rotate(-sneakTurnSpeed);
            }
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
    public override void Shoot()
    {

        if (shooter != null)
        {



            if (canShoot == true)
            {
                shooter.Shoot(bulletPrefab, bulletForce, damageDealt, bulletLifespan);
                noise.volumeDistance = 15;
                canShoot = false;
                shootCooldown = firerate;
            }


        }
        else
        {
            Debug.LogWarning("Warning: No Mover in TankPawn.MoveForward()!");
        }
    }

    public override void Sneak()
    {
        isSneaking = true;
    }

    public override void RotateTowards(Vector3 targetPosition)
    {
        //Find the vector to our target
        Vector3 targetVector = targetPosition - transform.position;
        //Find rotation to look down that vector
        Quaternion targetRotation = Quaternion.LookRotation(targetVector, Vector3.up);
        //Rotate closer to that vector at our turn speed
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
