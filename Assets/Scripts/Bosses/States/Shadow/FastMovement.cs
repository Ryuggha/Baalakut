using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FastMovement : State
{
    public State sweepAttack;
    private float distance;
    private float duration;
    private int movementsDone;
    private int movementsToSeep;

    private Vector3 origin;
    private bool walking = false;
    private Vector3 direction;

    public List<Vector3> posiblePositions;
    private NavMeshAgent agent;


    public void Start()
    {
        distance = ((Shadow)stateMachine).SlowMovementDistance;
        duration = ((Shadow)stateMachine).SlowMovementDuration;
        agent = go.GetComponent<NavMeshAgent>();
        movementsToSeep = ((Shadow)stateMachine).movementsToSweep;

    }
    public override State tick(float delta)
    {
        /*if (walking == false)
        {
            origin = go.transform.position;
            walking = true;
            do direction = new Vector3(Random.Range(-100, 101), 0, Random.Range(-100, 101)).normalized;
            while (direction == Vector3.zero);  //Must be a better way to do this
        }

        float auxVelocity = distance / duration;
        
        go.transform.position += direction * auxVelocity * delta;



        if (Vector3.Distance(origin, go.transform.position) >= distance)
        {
            walking = false;
            return go.GetComponentInChildren<PosMovement>().tick(delta);
        }

        return this;*/
        if (walking == false)
        {
            if (Random.Range(0, 3) == 0) SoundHandler.playSound("event:/SFX/Shadow/ShadowMovement", transform.position);
            walking = true;
            direction = posiblePositions[Random.Range(0, posiblePositions.Count)];
            agent.speed = distance / duration;
            agent.SetDestination(direction);
            agent.isStopped = false;
        }
        else if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance <= 0)
        {
            walking = false;
            agent.isStopped = true;
            movementsDone++;
            if (movementsDone >= movementsToSeep)
            {
                movementsDone = 0;
                return sweepAttack.tick(delta);
            }
            return go.GetComponentInChildren<PosMovement>();
        }

        return this;
    }

    public void setWalking(bool var)
    {
        walking = var;
    }

    public NavMeshAgent getAgent()
    {
        return this.agent;
    }
}
