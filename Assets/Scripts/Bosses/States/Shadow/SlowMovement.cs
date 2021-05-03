using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMovement : State
{
    public State nextState;

    private float distance;
    private float duration;

    private Vector3 origin;
    private bool walking;
    private Vector3 direction;


    public void Start()
    {
        distance = ((Shadow)stateMachine).SlowMovementDistance;
        duration = ((Shadow)stateMachine).SlowMovementDuration;



    }
    public override State tick(float delta)
    {
        if (walking == false)
        {
            origin = go.transform.position;
            walking = true;

            do direction = new Vector3(Random.Range(-100, 101), 0, Random.Range(-100, 101)).normalized;
            while (direction == Vector3.zero);  //Must be a better way to do this
           

        }

        float auxVelocity = distance / duration;
        

        go.transform.position += direction * auxVelocity * delta;
        if (Vector3.Distance(origin, go.transform.position)>= distance)
        {
            walking = false;
            return nextState;
        }

        return this;
    }
}
