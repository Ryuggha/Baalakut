using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulnerable : State
{
    public State nextState;

    private bool isVunerable = false;
    private float time = 0f;

    public override State tick(float delta)
    {
        if (!isVunerable)
        {
            isVunerable = true;
            time = 0;
        }

        time += delta;
        if (time >= ((Shadow)stateMachine).VulnerabilityDuration)
        {
            isVunerable = false;
            return nextState;
        }
        return this;
    }
}
