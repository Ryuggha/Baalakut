using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vulnerable : State
{

    private NavMeshAgent agent;
    private bool isVunerable = false;
    private float time = 0f;
    private LineRenderer ray;

    private void Start()
    {
        agent = go.GetComponent<NavMeshAgent>();
        ray = go.transform.GetChild(0).gameObject.GetComponent<LineRenderer>();
    }
    public override State tick(float delta)
    {
        if (!isVunerable)
        {
            isVunerable = true;
            time = 0;
            agent.isStopped = true;
            ray.enabled = false;
            go.GetComponentInChildren<SlowMovement>().setWalking(false);
            go.GetComponentInChildren<FastMovement>().setWalking(false);
            go.GetComponentInChildren<Shoot>().setShootting(false);
            go.GetComponentInChildren<LoadShot>().setActive(false);
            go.GetComponentInChildren<Aiming>().setAiming(false);
        }

        time += delta;
        if (time >= ((Shadow)stateMachine).VulnerabilityDuration)
        {
            isVunerable = false;
            return go.GetComponentInChildren<FastMovement>();
        }
        return this;
    }
}
