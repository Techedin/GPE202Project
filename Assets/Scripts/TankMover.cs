using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : Mover
{
    private Rigidbody rb;
    // Start is called before the first frame update
    public override void Start()
    {
        //The body of our tank
        rb = GetComponent<Rigidbody>();
    }

    public override void Move(Vector3 direction, float speed)
    {
        //Makes sure movement is based in time not dependent on Frames
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVector);

    }

    public override void Rotate(float turnSpeed)
    {
        //Rotate based on time not dependent on frames
        rb.transform.Rotate(0, 1 * turnSpeed * Time.deltaTime, 0);
    }
}
