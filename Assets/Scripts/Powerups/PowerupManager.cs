using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public Pawn pawn;
    public List<Powerup> activePowerups;
    public List<Powerup> permanentPowerups;
    private List<Powerup> removedPowerups;
    // Start is called before the first frame update
    void Start()
    {
        activePowerups = new List<Powerup>();
        permanentPowerups = new List<Powerup>();
        removedPowerups = new List<Powerup>();

    }

    // Update is called once per frame
    void Update()
    {
        PowerupsTimer();
    }

    private void LateUpdate()
    {
        RemovePowerups();
    }

    public void Add(Powerup powerupAdd)
    {
        powerupAdd.Apply(this);
    }

    public void Remove(Powerup powerupRemove)
    {

       activePowerups.Remove(powerupRemove);

        removedPowerups.Add(powerupRemove);

        powerupRemove.Remove(this);
    }

    private void RemovePowerups()
    {
        foreach (Powerup powerup in removedPowerups)
        {
            removedPowerups.Remove(powerup);
        }
        removedPowerups.Clear();
    }

    public void PowerupsTimer()
    {
        foreach (Powerup powerup in activePowerups)
        {
            Debug.Log("test");
            powerup.powerupTime -= Time.deltaTime;
            if (powerup.powerupTime <= 0)
            {
                Remove(powerup);
            }
        }
    }
}
