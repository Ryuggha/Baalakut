using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadShot : State
{
    public State nextState;

    private float timeLeft;
    private bool active = false;

    public override State tick(float delta)
    {
        if (!active)
        {
            active = true;
            timeLeft = ((Shadow)stateMachine).TimeBetweenAimingAndShots;
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
