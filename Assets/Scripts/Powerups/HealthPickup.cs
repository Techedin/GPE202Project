using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public HealthPowerup powerup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        //Try and grab a powerup manager
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();
        //Check to see if it has one
        if(powerupManager != null)
        {
            if (powerup.isPermanent == true)
            {
                powerupManager.permanentPowerups.Add(powerup);
                powerupManager.Add(powerup);
               
            }
            else
            {
                powerupManager.Add(powerup);
                powerupManager.activePowerups.Add(powerup);
            }
            //If you wanted to respawn it then just set in inactive then after a timer set it active again
            Destroy(gameObject);
        }

    }
}
