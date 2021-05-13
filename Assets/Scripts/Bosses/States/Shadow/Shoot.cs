using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : State
{
    

    private Transform eye;
    private LineRenderer ray;

    private float timeShot = 0f;

    private bool shootting = false;



    private void Start()
    {
        eye = go.transform.GetChild(0);
        ray = eye.gameObject.GetComponent<LineRenderer>();
    }

    public override State tick(float delta)
    {
        RaycastHit hit;
        Ray rayCast =new Ray();

        if (!shootting)
        {
            shootting = true;
            timeShot = 0f;
            eye.gameObject.GetComponent<LineRenderer>().enabled = true;
            ray.startWidth = ((Shadow)stateMachine).ShotRayWidth;
            ray.endWidth = ((Shadow)stateMachine).ShotRayWidth;

            

            rayCast = new Ray(ray.GetPosition(0), ray.GetPosition(1)-ray.GetPosition(0));
            

            
            
            if (Physics.Raycast(rayCast, out hit, 100, LayerMask.GetMask("Player")))
            {
                ((Shadow)stateMachine).player.GetComponentInChildren<PlayerManager>().takeDamage();
            }

            
        }

        

        timeShot += delta;
       
        if (timeShot >= ((Shadow)stateMachine).ShotDuration)
        {
            eye.gameObject.GetComponent<LineRenderer>().enabled = false;
            shootting = false;
            return go.GetComponentInChildren<PreMovement>();
        }
        else return this;
        
    }

    public void setShootting(bool var)
    {
        shootting = var;
    }

}
