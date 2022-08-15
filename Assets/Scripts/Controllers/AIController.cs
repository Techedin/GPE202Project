using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    
    public enum AIStates { Guard, Chase, Flee, Patrol, Attack, Scan, ChooseTarget }
    public AIStates currentState;
    [SerializeField] protected float lastStateChangedTime;

    public GameObject target;

    public float fleeDistance;
    public float hearingDistance;

    public float fieldOfView;
    public float viewDistance;

    public Transform lastHeardLocation;

    //Hold our waypoints in a list and allows developers to change which locations to move to
    public Transform[] waypoints;
    public float waypointStopDistance;
    private int currentWaypoint = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        ChangeState(AIStates.ChooseTarget);
    }

    // Update is called once per frame
    public override void Update()
    { 
        
        MakeDecisions();
        CanSee(target);

        lastStateChangedTime += Time.deltaTime;
        base.Update();
    }


    public  virtual void MakeDecisions()
    {
        switch (currentState)
        {
            case AIStates.Guard:
                DoGuardState();
                
                if (IsHasTarget() != true)
                {
                    ChangeState(AIStates.ChooseTarget);

                }
                if (CanHear(target) == true)
                {
                    pawn.RotateTowards(lastHeardLocation.position);
                }
                if(CanSee(target) == true)
                {
                    Chase(target.transform.position);
                
                    ChangeState(AIStates.Chase);
                }
                if (pawn.health.currentHealth <= 25)
                {
                    ChangeState(AIStates.Flee);
                }
                if (lastStateChangedTime >= 5)
                {  
                    ChangeState(AIStates.Scan);
                }
                if (IsDistanceLessThan(target, 8))
                {
                    ChangeState(AIStates.Chase);
                }

                break;
            case AIStates.Scan:
                //Do tasks for Scan
                DoScanState();
                //Check for transitions
                if (IsHasTarget() != true)
                {
                    ChangeState(AIStates.ChooseTarget);

                }
                if (CanHear(target) == true)
                {
                    ChangeState(AIStates.Guard);
                }
                if (CanSee(target) == true)
                {
               
                    ChangeState(AIStates.Chase);
                }

                if (pawn.health.currentHealth <= 25)
                {
                    ChangeState(AIStates.Flee);
                }
                if (IsHasTarget() != true)
                {
                    ChangeState(AIStates.ChooseTarget);

                }
                if (IsDistanceLessThan(target, 8))
                {
                    ChangeState(AIStates.Chase);
                }
                if (lastStateChangedTime >= 5)
                {
                    currentWaypoint = 0;
                    ChangeState(AIStates.Patrol);
                }
                break;
            case AIStates.Chase:
                //Do tasks for Chase
                DoChaseState();
                if (IsHasTarget() == false)
                {
                    ChangeState(AIStates.ChooseTarget);

                }
                if (!IsDistanceLessThan(target,15) && CanSee(target) == false)
                {
                    ChangeState(AIStates.Scan);
                }
                if (IsDistanceLessThan(target, 5) && CanSee(target) == true)
                {
                    ChangeState(AIStates.Attack);
                }
                if (pawn.health.currentHealth <= 25)
                {
                    ChangeState(AIStates.Flee);
                }
                break;
            case AIStates.Attack:
                DoAttackState();

                if (!IsDistanceLessThan(target, 5) && CanSee(target) == false)
                {
                    ChangeState(AIStates.Chase);
                }
                if (pawn.health.currentHealth <= 25)
                {
                    ChangeState(AIStates.Flee);
                }
                if (IsHasTarget() == false)
                {
                    ChangeState(AIStates.ChooseTarget);

                }
                break;
            case AIStates.Patrol:
                Patrol();

              
                if (IsHasTarget() == false)
                {
                    ChangeState(AIStates.ChooseTarget);

                }  
                if (CanHear(target) == true)
                {
                    ChangeState(AIStates.Guard);
                }
                if (CanSee(target) == true)
                {
                 
                    ChangeState(AIStates.Chase);
                }
                if (IsDistanceLessThan(target, 6))
                {
                    ChangeState(AIStates.Chase);
                }
                break;
            case AIStates.Flee:
                Flee();

                if (!IsDistanceLessThan(target, 25))
                {
                    pawn.health.Heal(15);
                    ChangeState(AIStates.Guard);
                }
                if (IsHasTarget() == false)
                {
                    ChangeState(AIStates.ChooseTarget);

                }
                break;
            case AIStates.ChooseTarget:
                TargetPlayerOne();
                if (IsHasTarget() == true)
                {
                    ChangeState(AIStates.Patrol);
                }
                break;

        }


    }

    public virtual void ChangeState(AIStates newState)
    {
        //Change Current State
        currentState = newState;
        //Set the time to zero so I can track to time from the last state change in the update
        lastStateChangedTime = 0;

    }
    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


   public bool CanHear(GameObject target)
    {
        //Get our target's NoiseMaker
        NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();
        //Check to see if we got one
        if (noiseMaker == null)
        {
            return false;
        }
        //If they are making zero noise, they also can't be heard
        if (noiseMaker.volumeDistance <= 0)
        {
            return false;
        }
        //Check to see if noise made overlaps hearing distance
        float totalDistance = (float)noiseMaker.volumeDistance + hearingDistance;
        if(Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
        {
            lastHeardLocation = target.transform;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanSee(GameObject target)
    {
        // Find the vector from the agent to the target
        Vector3 agentToTargetVector = target.transform.position - pawn.transform.position;
        // Find the angle between the direction our agent is facing (forward in local space) and the vector to the target.
        float angleToTarget = Vector3.Angle(agentToTargetVector, pawn.transform.forward);
        // if that angle is less than our field of view
        if (angleToTarget <= fieldOfView)
        {
           
            Ray fovCheck = new Ray(pawn.transform.position, agentToTargetVector);
            Debug.DrawRay(pawn.transform.position, agentToTargetVector);
            Physics.Raycast(fovCheck, out RaycastHit hit, viewDistance);
        
            if (hit.rigidbody == target.GetComponent<Rigidbody>())
            {
                Debug.Log("player seen");
               
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    protected virtual void DoChaseState()
    {
        CanSee(target);
        Chase(target);
       
    }
    protected virtual void DoGuardState()
    {
        CanSee(target);
        pawn.RotateTowards(lastHeardLocation.transform.position);
    }
    protected virtual void DoAttackState()
    {
        CanSee(target);
        Chase(target);
        Shoot();
    }
    protected virtual void DoChooseTargetState()
    {
        //There is more targeting but this is the base
        TargetPlayerOne();
    }
    protected virtual void DoScanState()
    {
        CanSee(target);
        pawn.MoveLeft();
      
    }



    public void Chase(Vector3 targetPosition)
    {

        //Using the movement we coded in the pawn
        pawn.RotateTowards(targetPosition);
        pawn.MoveForward();
    }
    public void Chase(Pawn targetPawn)
    {
        //Overloading
        Chase(targetPawn.transform.position);
    }
    public void Chase(Transform targetTransform)
    {
        //Overloading
        Chase(targetTransform.transform.position);
    }
    public void Chase(GameObject target)
    {
        //Overloading
        Chase(target.transform.position);

    }
    protected void Flee()
    {
        //Find vector to our target
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        //find the vector complete opposite from our target
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        //Normalize it and provide magnitude
        Vector3 fleeVector = vectorAwayFromTarget.normalized * fleeDistance;

        Chase(pawn.transform.position - fleeVector);
    }


    protected virtual void Patrol()
    {
        CanSee(target);
        if (waypoints.Length > currentWaypoint)
        {
            Chase(waypoints[currentWaypoint]);

            //Check distance between our pawn and our waypoint to see if we reached the waypoint within the waypoint stop distance
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)
            {
                currentWaypoint++;
            }


        }
        else
        {
            RestartPatrol();
        }
    }
    protected void RestartPatrol()
    {
        currentWaypoint = 0;
    }


    public void Shoot()
    {
        pawn.Shoot();
    }



    protected bool IsHasTarget()
    {
        //returns true if we have a target, false if we don't
        return (target != null);
    }

    protected void TargetPlayerOne()
    {
        //Check for GameManager
        if (GameManager.gameManagerInstance != null)
        {
            //And the array of player exists
            if (GameManager.gameManagerInstance != null)
            {
                //that there is a person in the list
                if (GameManager.gameManagerInstance.players.Count > 0)
                {
                    //target the first player in the list
                    target = GameManager.gameManagerInstance.players[0].pawn.gameObject;
                }
            }
        }
    }

    protected void TargetNearestTank()
    {
        //Get All Tanks Pawns
        Pawn[] allTanks = FindObjectsOfType<Pawn>();
        //Set a default closest tank to compair to
        Pawn closestTank = allTanks[0];

        float closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);

        foreach(Pawn tank in allTanks)
        {
            //Check distance between all the tank. If closer then assign that tank as closest
            if(Vector3.Distance(pawn.transform.position, tank.transform.position) <= closestTankDistance)
            {
                closestTank = tank;
                closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);
            }
        }
        target = closestTank.gameObject;
    }

    protected void TargetHealthiestTank()
    {
        //Get All Tanks Pawns
        Pawn[] allTanks = FindObjectsOfType<Pawn>();
        //Set a default healthiest tank to compair to
        Pawn healthiestTank = allTanks[0];

        float healthiestTankHealth = healthiestTank.health.currentHealth;

        foreach (Pawn tank in allTanks)
        {
            //Check health between all the tank. If healthier then assign that tank as healthiest
            if (tank.health.currentHealth >= healthiestTankHealth)
            {
                healthiestTank = tank;
                healthiestTankHealth = healthiestTank.health.currentHealth;
            }
        }
        target = healthiestTank.gameObject;
    }

    protected void TargetHealthiaintTank()
    {
        //Get All Tanks Pawns
        Pawn[] allTanks = FindObjectsOfType<Pawn>();
        //Set a default healthiest tank to compair to
        Pawn healthiaintTank = allTanks[0];

        float healthiaintTankHealth = healthiaintTank.health.currentHealth;

        foreach (Pawn tank in allTanks)
        {
            //Check health between all the tank. If healthier then assign that tank as healthiest
            if (tank.health.currentHealth <= healthiaintTankHealth)
            {
                healthiaintTank = tank;
                healthiaintTankHealth = healthiaintTank.health.currentHealth;
            }
        }
        target = healthiaintTank.gameObject;
    }

  



}
