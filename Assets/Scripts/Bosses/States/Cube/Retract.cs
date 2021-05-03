using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retract : State
{
    public State nextState;
    public override State tick(float delta)
    {
        Vector3 aux = ((Cube)stateMachine).back.transform.localPosition;
        aux.z += ((Cube)stateMachine).retractVelocity * delta;
        bool reachedDestinacion = false;
        if (aux.z > 0)
        {
            aux.z = 0;
            reachedDestinacion = true;
        }
        ((Cube)stateMachine).back.transform.localPosition = aux;

        if (reachedDestinacion) return nextState;
        return this;
    }
}