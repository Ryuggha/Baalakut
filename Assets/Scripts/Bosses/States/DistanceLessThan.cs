using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceLessThan : State
{
    public State nextState;

    public float minDistance;

    public override State tick(float delta)
    {
        if (Vector3.Distance(stateMachine.player.transform.position, stateMachine.transform.position) < minDistance) return nextState.tick(delta);
        return this;
    }
}
