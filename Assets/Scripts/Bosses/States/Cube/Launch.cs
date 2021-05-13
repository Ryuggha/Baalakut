using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : State
{
    [HideInInspector] public bool gonnaStun;
    public State nextState;
    public override State tick(float delta)
    {
        Vector3 aux = ((Cube)stateMachine).front.transform.localPosition;
        aux.z += ((Cube)stateMachine).launchVelocity * delta;
        float distanceToMove = ((Cube)stateMachine).getDistanceToMove();
        bool reachedDestinacion = false;
        if (aux.z > distanceToMove)
        {
            aux.z = distanceToMove;
            reachedDestinacion = true;
        }
        ((Cube)stateMachine).front.transform.localPosition = aux;

        if (reachedDestinacion)
        {
            if (gonnaStun) ((Cube)stateMachine).stunned();
            gonnaStun = false;
            return nextState;
        }
        return this;
    }
}
