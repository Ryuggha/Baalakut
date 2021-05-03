using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : State
{
    public State nextState;

    private Transform eye;
    private LineRenderer ray;
    private State vulnerableState;

    private float timeShot = 0f;

    private bool shootting = false;

    private bool makeItVulnerable = false;

    private void Start()
    {
        eye = go.transform.GetChild(0);
        ray = eye.gameObject.GetComponent<LineRenderer>();
        vulnerableState = go.GetComponentInChildren<Vulnerable>();
    }

    public override State tick(float delta)
    {

        if (!shootting)
        {
            shootting = true;
            timeShot = 0f;
            eye.gameObject.GetComponent<LineRenderer>().enabled = true;
            ray.startWidth = ((Shadow)stateMachine).ShotRayWidth;
            ray.endWidth = ((Shadow)stateMachine).ShotRayWidth;

            RaycastHit hit;

            Ray rayCast = new Ray(ray.GetPosition(0), (ray.GetPosition(1)-ray.GetPosition(0)));

            
            if (Physics.Raycast(rayCast, out hit, 100, LayerMask.GetMask("Cristal")))  
            {
                Debug.Log("Cristal Hited");
                MakeItVulnerable();
            }
            else if (Physics.Raycast(rayCast, out hit, 100, LayerMask.GetMask("Player")))
            {
                Debug.Log("PLayer hitted");
            }
            
        }

        if (makeItVulnerable)
        {
            makeItVulnerable = false;
            eye.gameObject.GetComponent<LineRenderer>().enabled = false;
            shootting = false;
            Debug.Log("OH NO IM VULNERABLE");
            return vulnerableState;
        }

        timeShot += delta;
       
        if (timeShot >= ((Shadow)stateMachine).ShotDuration)
        {
            eye.gameObject.GetComponent<LineRenderer>().enabled = false;
            shootting = false;
            return nextState;
        }
        else return this;
        
    }


    public void MakeItVulnerable()
    {
        makeItVulnerable = true;
    }
}
