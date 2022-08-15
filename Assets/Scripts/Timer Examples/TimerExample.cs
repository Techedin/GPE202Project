using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerExample : MonoBehaviour
{
     public float timerDelay = 1.0f;
    private float nextEventTime;
    // Start is called before the first frame update
    void Start()
    {
        nextEventTime = Time.time + timerDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextEventTime)
        {
            Debug.Log("It’s me!");
            nextEventTime = Time.time + timerDelay;
        }
    }
}
