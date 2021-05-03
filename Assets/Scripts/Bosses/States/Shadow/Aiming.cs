using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : State
{
    public State nextState;

    private Transform eye;
    private LineRenderer ray;

    private float timeAiming = 0f;

    private bool aiming = false;

    

    private void Start()
    {
        eye = go.transform.GetChild(0);
        ray = eye.gameObject.GetComponent<LineRenderer>();
    }

    public override State tick(float delta)
    {
        eye.LookAt(((Shadow)stateMachine).player.transform);

        if (!aiming)
        {
            aiming = true;
            timeAiming = 0f;
            eye.gameObject.GetComponent<LineRenderer>().enabled = true;
            ray.startWidth = ((Shadow)stateMachine).AimingRayWidth;
            ray.endWidth = ((Shadow)stateMachine).AimingRayWidth;
        }

        ray.SetPosition(0, eye.transform.position);
        ray.SetPosition(1, ((Shadow)stateMachine).player.transform.position + Vector3.up * 1f);

        timeAiming += delta;

        if (timeAiming >= ((Shadow)stateMachine).AimingDuration)
        {
            aiming = false;
            return nextState;
        }
        else return this;
        
        
        
    }

  
}
