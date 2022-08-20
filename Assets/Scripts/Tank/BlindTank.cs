using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindTank : AIController
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

     public override void MakeDecisions()
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
                    ChangeState(AIStates.Attack);
                }
                if (pawn.health.currentHealth <= 25)
                {
                    ChangeState(AIStates.Flee);
                }

                break;
            case AIStates.Attack:
                DoAttackState();

                if (lastStateChangedTime >= 5)
                {
                    ChangeState(AIStates.Guard);
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
                    ChangeState(AIStates.Guard);
                }
                break;

        }


    }
    protected override void DoGuardState()
    {

    }

    new protected void DoAttackState()
    {
        pawn.RotateTowards(lastHeardLocation.position);
        Shoot();
    }

    new protected void Flee()
    {
        //Find vector to our target
        Vector3 vectorToTarget = lastHeardLocation.transform.position - pawn.transform.position;
        //find the vector complete opposite from our target
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        //Normalize it and provide magnitude
        Vector3 fleeVector = vectorAwayFromTarget.normalized * fleeDistance;

        Chase(pawn.transform.position - fleeVector);
    }


}
