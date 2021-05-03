using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreRetract : State
{
    public override State tick(float delta)
    {
        ((Cube)stateMachine).back.transform.localPosition = new Vector3(((Cube)stateMachine).back.transform.localPosition.x, ((Cube)stateMachine).back.transform.localPosition.y, -((Cube)stateMachine).getDistanceToMove());
        Vector3 aux = go.transform.position;
        aux += (go.transform.forward * ((Cube)stateMachine).getDistanceToMove());
        go.transform.position = aux;
        aux = ((Cube)stateMachine).front.transform.localPosition;
        aux.z = 0;
        ((Cube)stateMachine).front.transform.localPosition = aux;
        return go.GetComponentInChildren<Retract>();
    }
}
