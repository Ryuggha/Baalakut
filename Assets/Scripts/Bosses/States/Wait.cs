using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait : State
{
    public State nextState;
    public float timeToWait;

    private float timeLeft;
    private bool active = false;

    public override State tick(float delta)
    {
        if (!active)
        {
            active = true;
            timeLeft = timeToWait;
        }

        timeLeft -= delta;

        if (timeLeft <= 0)
        {
            active = false;
            return nextState.tick(delta);
        }
        return this;
    }
}
