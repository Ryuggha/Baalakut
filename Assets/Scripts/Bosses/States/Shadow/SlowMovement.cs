using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlowMovement : State
{

    private float distance;
    private float duration;

    private Vector3 origin;
    private bool walking = false;
    private Vector3 direction;

    private float[] zAxis = { 19f, 4.5f, 8f, -20f }; 
    private float[] xAxis = { 20f, 1f, 22f };
    private float yAxis = 0.2f;
    private NavMeshAgent agent;


    public void Start()
    {
        distance = ((Shadow)stateMachine).SlowMovementDistance;
        duration = ((Shadow)stateMachine).SlowMovementDuration;
        agent = go.GetComponent<NavMeshAgent>();


    }

    public override State tick(float delta)
    {
        /* if (walking == false)
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
             return go.GetComponentInChildren<PosMovement>().tick(delta);
         }

         return this;
         */


        if (walking == false)
        {
            walking = true;
            direction = new Vector3(xAxis[Random.Range(0, 3)], yAxis, zAxis[Random.Range(0, 4)]);
            agent.speed = distance / duration;
            agent.SetDestination(direction);
            agent.isStopped = false;
        }

        else if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0)
        {
            walking = false;
            agent.isStopped = true;
            return go.GetComponentInChildren<PosMovement>();

        }

        return this;
    }

    public void setWalking(bool var)
    {
        walking = var;
    }
}
