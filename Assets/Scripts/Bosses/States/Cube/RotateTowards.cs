using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : State
{
    public override State tick(float delta)
    {
        float angleToTarget = Vector3.Angle(go.transform.forward, stateMachine.player.transform.position - go.transform.position);
        Vector3 crossToTarget = Vector3.Cross(go.transform.forward, stateMachine.player.transform.position - go.transform.position);

        if (angleToTarget > 135) rotate(2);
        else if (angleToTarget > 45)
        {
            if (crossToTarget.y > 0) rotate(1);
            else rotate(-1);
        }

        return go.GetComponentInChildren<PreLaunch>().tick(delta);
    }

    private void rotate(int n)
    {
        float actualAngle = go.transform.rotation.eulerAngles.y;
        actualAngle += 90 * n;
        go.transform.rotation = Quaternion.Euler(0, actualAngle, 0);
    }
}
