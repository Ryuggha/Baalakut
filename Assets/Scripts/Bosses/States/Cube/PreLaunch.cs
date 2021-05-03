using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLaunch : State
{
    public override State tick(float delta)
    {
        if (go.transform.rotation.eulerAngles.y < 10 || (go.transform.rotation.eulerAngles.y > 140 && go.transform.rotation.eulerAngles.y < 200))
        {
            ((Cube)stateMachine).setDistanceToMove(Mathf.Abs(stateMachine.player.transform.position.z - go.transform.position.z));
        }
        else
        {
            ((Cube)stateMachine).setDistanceToMove(Mathf.Abs(stateMachine.player.transform.position.x - go.transform.position.x));
        }
        return go.GetComponentInChildren<Launch>();
    }
}
