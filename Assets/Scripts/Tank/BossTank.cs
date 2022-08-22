using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTank : AIController
{

    public override void Start()
    {
        base.Start();
    }

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
                }
                if (CanSee(target) == true)
                {
                    ChangeState(AIStates.Chase);
                }
                if (pawn.health.currentHealth <= 25)
                {
                    ChangeState(AIStates.Flee);
                }
                if (IsDistanceLessThan(target, 15))
                {
                    ChangeState(AIStates.Chase);
                }

                break;
            case AIStates.Chase:
                //Do tasks for Chase
                DoChaseState();
                if (IsHasTarget() == false)
                {
                    ChangeState(AIStates.ChooseTarget);

                }
                if (IsDistanceLessThan(target, 10))
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

                if (IsHasTarget() != true)
                {
                    ChangeState(AIStates.ChooseTarget);

                }
                if (!IsDistanceLessThan(target, 5))
                {
                    ChangeState(AIStates.Chase);
                }
                if (pawn.health.currentHealth <= 25)
                {
                    ChangeState(AIStates.Flee);
                }
              
                break;

            case AIStates.Flee:
                DoBossEnraged();

                if (IsHasTarget() != true)
                {
                    ChangeState(AIStates.ChooseTarget);

                }
                if (!IsDistanceLessThan(target, 25))
                {
                    pawn.health.Heal(15);
                    ChangeState(AIStates.Guard);
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
        Chase(target);
        Shoot();
    }

    protected void DoBossEnraged()
    {
        pawn.MoveLeft();
        pawn.firerate = 0.2f;
        Shoot();
    }
}

