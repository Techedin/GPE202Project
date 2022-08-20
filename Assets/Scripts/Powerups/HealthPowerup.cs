using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class HealthPowerup : Powerup
{
    public float healthToAdd;
 

    public override void Apply(PowerupManager target)
    {
        target.pawn.health.maxHealth += healthToAdd;
        target.pawn.health.Heal(healthToAdd);
    }
    public override void Remove(PowerupManager target)
    {

    }

    
}
