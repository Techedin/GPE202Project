using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    //Gotta make the parent class so all the children can have cognitive functions 
    //Varible for move speed
    public float moveSpeed;
    public float sneakSpeed;
    //Varible for turn speed
    public float turnSpeed;
    public float sneakTurnSpeed;

    public bool isSneaking; 
    //Varible to hold our Mover Script or Childs? of Mover
    public Mover mover;
    //Shooting Varibles
    public Shooter shooter;
    //Health Comp
    public Health health;

    public NoiseMaker noise;

    public GameObject bulletPrefab;
    public float bulletForce;
    public float damageDealt;
    public float bulletLifespan;
    public float firerate;

    // Start is called before the first frame update
    public virtual void Start()
    {
        mover = GetComponent<Mover>();

        shooter = GetComponent<Shooter>();

        health = GetComponent<Health>();

        noise = GetComponent<NoiseMaker>();
       
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(noise.volumeDistance >= 0)
        {
            noise.volumeDistance -= Time.deltaTime;
        }
        
    }

    public abstract void MoveForward();
    public abstract void MoveBackwards();
    public abstract void MoveRight();
    public abstract void MoveLeft();
    public abstract void Jump();
    public abstract void Shoot();
    public abstract void Sneak();
    public abstract void RotateTowards(Vector3 targetPosition);
}
