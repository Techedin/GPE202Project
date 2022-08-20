using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireratePickup : MonoBehaviour
{
    public FireratePowerup fireratePowerup;
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
        if (powerupManager != null)
        {
            
            if (fireratePowerup.isPermanent == true)
            {
                powerupManager.permanentPowerups.Add(fireratePowerup);
                powerupManager.Add(fireratePowerup);
                
            }
            else
            {
                powerupManager.activePowerups.Add(fireratePowerup);
                powerupManager.Add(fireratePowerup);
             
            }
            Destroy(gameObject);

        }

    }
}
