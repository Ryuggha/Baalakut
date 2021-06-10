using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLaunch : State
{
    public LayerMask wallLayers;

    [Header("Raycast offsets")]
    public Vector3[] raycastPoints = { new Vector3(0, 3, 0), 
                                       new Vector3(0, 3, 2),
                                       new Vector3(0, 3, -2),
                                       new Vector3(2, 3, 0), 
                                       new Vector3(-2, 3, 0)}; 

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
        float distance, lastDistance = -1;
        foreach (Vector3 pointOffset in raycastPoints) {
            if (Physics.Raycast(stateMachine.gameObject.transform.position + pointOffset, stateMachine.transform.forward, out hit, ((Cube)stateMachine).getDistanceToMove() + ((Cube)stateMachine).front.transform.localScale.x, wallLayers))
            {
                distance = Vector3.Distance(stateMachine.gameObject.transform.position, hit.point) - ((Cube)stateMachine).front.transform.localScale.x;
                if (lastDistance == -1) {
                    lastDistance = Vector3.Distance(stateMachine.gameObject.transform.position, hit.point) - ((Cube)stateMachine).front.transform.localScale.x;
                    ((Cube)stateMachine).setDistanceToMove(distance, false);
                    go.GetComponentInChildren<Launch>().gonnaStun = true;
                } else if (lastDistance > distance)
                {
                    ((Cube)stateMachine).setDistanceToMove(distance, false);
                    go.GetComponentInChildren<Launch>().gonnaStun = true;
                }
            }
        }

        return go.GetComponentInChildren<Launch>();
    }
}
