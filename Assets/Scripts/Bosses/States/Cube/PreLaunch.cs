using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLaunch : State
{
    public LayerMask wallLayers;

    public override State tick(float delta)
    {
        if (go.transform.rotation.eulerAngles.y < 10 || (go.transform.rotation.eulerAngles.y > 140 && go.transform.rotation.eulerAngles.y < 200))
        {
            ((Cube)stateMachine).setDistanceToMove(Mathf.Abs(stateMachine.player.transform.position.z - go.transform.position.z), true);
        }
        else
        {
            ((Cube)stateMachine).setDistanceToMove(Mathf.Abs(stateMachine.player.transform.position.x - go.transform.position.x), true);
        }

        RaycastHit hit;
        if (Physics.Raycast(stateMachine.gameObject.transform.position, stateMachine.transform.forward, out hit, ((Cube)stateMachine).getDistanceToMove() + ((Cube)stateMachine).front.transform.localScale.x, wallLayers))
        {
            ((Cube)stateMachine).setDistanceToMove(Vector3.Distance(stateMachine.gameObject.transform.position, hit.point) - ((Cube)stateMachine).front.transform.localScale.x, false   );
        }

        return go.GetComponentInChildren<Launch>();
    }
}
