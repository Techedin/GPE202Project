using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class FireratePowerup : Powerup
{
    //The base fire rate is 1 shoot/sec so decrease it by .5 to doublaaaae firerate
    public float firerateIncreaseAmount;
    public override void Apply(PowerupManager target)
    {
        target.pawn.firerate -= firerateIncreaseAmount;
    }
    public override void Remove(PowerupManager target)
    {
        //reversing what I subtracted from firerate
        target.pawn.firerate += firerateIncreaseAmount;
    }
}
