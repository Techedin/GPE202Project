using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    //refernce to our pawn
    public Pawn pawn;
    public int livesTotal;
    public int pointTotal;
    // Initializing functions for our parent controller(the worst type of parents..Controlling)
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void ProcessInputs()
    {

    }

    public virtual void OnDied()
    {
        if (livesTotal <= 0)
        {
            Destroy(pawn.gameObject);
          
        }
    }
    public virtual void AddPoints(int pointTotal)
    {

    }
}
