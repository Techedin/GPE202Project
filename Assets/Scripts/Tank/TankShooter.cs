using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : Shooter
{

    public Transform bulletSpawn;

    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void Shoot(GameObject bulletPrefab, float bulletForce, float damageDealt, float bulletLifespan)
    {
        //Instantiate the shell,bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
        //Grab OnHit comp
        OnHit onHitComp = bullet.GetComponent<OnHit>();

        //Check for OnHit comp

        if(onHitComp != null)
        {
            //set damage value to the value damageDealt in the OnHit Script
            onHitComp.damageDealt = damageDealt;

            //set owner
            onHitComp.owner = GetComponent<Pawn>();
        }

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        //If the bullet has one
        if(rb != null)
        {
            rb.AddForce(bulletSpawn.forward * bulletForce);
        }
        //Destroy bullet after time
        Destroy(bullet, bulletLifespan);
    }
}
