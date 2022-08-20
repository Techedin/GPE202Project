using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTank : AIController
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
             
                if (CanSee(target) == true)
                {
                    ChangeState(AIStates.Attack);
                }
               

                break;
            case AIStates.Attack:
                DoAttackState();

                if (lastStateChangedTime >= 5)
                {
                    ChangeState(AIStates.Guard);
                }
               
                if (IsHasTarget() == false)
                {
                    ChangeState(AIStates.ChooseTarget);

                }
                break;
            case AIStates.Flee:
              
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
        CanSee(target);
        pawn.MoveLeft();
    }

    new protected void DoAttackState()
    {
        pawn.RotateTowards(target.transform.position);
        Shoot();
    }

  
}
