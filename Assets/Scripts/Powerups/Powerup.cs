using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup
{
    public float powerupTime;
    public bool isPermanent;
    public abstract void Apply(PowerupManager target);
    public abstract void Remove(PowerupManager target);
}
