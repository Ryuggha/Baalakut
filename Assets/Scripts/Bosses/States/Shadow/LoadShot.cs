using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadShot : State
{

    private float timeLeft;
    private bool active = false;
    private Ray rayCast = new Ray();
    private LineRenderer ray;

    private void Start()
    {
        ray = go.transform.GetChild(0).gameObject.GetComponent<LineRenderer>();
    }


    public override State tick(float delta)
    {
        if (!active)
        {
            active = true;
            timeLeft = ((Shadow)stateMachine).TimeBetweenAimingAndShots;
        }

        rayCast = new Ray(ray.GetPosition(0), ray.GetPosition(1) - ray.GetPosition(0));
        RaycastHit hit;
        Physics.Raycast(rayCast, out hit);
        ray.SetPosition(1, hit.point);

        timeLeft -= delta;

        if (timeLeft <= 0)
        {
            active = false;
            return go.GetComponentInChildren<Shoot>().tick(delta);
        }
        return this;
    }

    public void setActive(bool var)
    {
        active = var;
    }
}
