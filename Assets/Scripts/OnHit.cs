using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour
{
    public float damageDealt;
    public Pawn owner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider hit)
    {

        //Get Health comp. from hit collider if it has one
        Health hitHealth = hit.GetComponent<Health>();

        //check for Health comp
        if(hitHealth != null)
        {
            hitHealth.TakeDamage(damageDealt, owner);
        }
        

        //For memory reasons not personal 
        Destroy(gameObject);
    }
}
