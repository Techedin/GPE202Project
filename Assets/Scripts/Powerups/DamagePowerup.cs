using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DamagePowerup : Powerup
{
    public float damageIncreaseAmount;
    public override void Apply(PowerupManager target)
    {
        target.pawn.damageDealt += damageIncreaseAmount;
    }
    public override void Remove(PowerupManager target)
    {
        //reversing what I subtracted from firerate
        target.pawn.damageDealt -= damageIncreaseAmount;
    }
}
