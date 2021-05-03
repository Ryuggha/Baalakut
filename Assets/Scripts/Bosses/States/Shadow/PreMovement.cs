using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreMovement : State
{
    public override State tick(float delta)
    {
       
        int random = Random.Range(0, 100);
        if (random >= ((Shadow)stateMachine).SlowWalkChance)
        {
            return go.GetComponentInChildren<FastMovement>().tick(delta);
        }
        else
        {
            return go.GetComponentInChildren<SlowMovement>().tick(delta);
        }
    }
}
