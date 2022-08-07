using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    //Gotta make the parent class so all the children can have cognitive functions 
    //Varible for move speed
    public float moveSpeed;
    //Varible for turn speed
    public float turnSpeed;

    //Varible to hold our Mover Script or Childs? of Mover
    public Mover mover;

    // Start is called before the first frame update
    public virtual void Start()
    {
        mover = GetComponent<Mover>();
      

    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public abstract void MoveForward();
    public abstract void MoveBackwards();
    public abstract void MoveRight();
    public abstract void MoveLeft();
    public abstract void Jump();
}
