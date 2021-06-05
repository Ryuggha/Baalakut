using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : State
{
    private Transform eye;
   
    private LineRenderer ray;
    private GameObject playerChaser;
    private float playerChaser_Speed;
    private Transform target;

    


    private float timeAiming = 0f;

    private bool aiming = false;
    private Color color;

    private void Start()
    {
        eye = go.transform.GetChild(0);
        ray = eye.gameObject.GetComponent<LineRenderer>();
        color = ((Shadow)stateMachine).aimingColor;
        playerChaser = ((Shadow)stateMachine).playerChaser;
    }

    public override State tick(float delta)
    {
        eye.LookAt(((Shadow)stateMachine).player.transform);

        if (!aiming)
        {
            SoundHandler.playSound("event:/SFX/Shadow/LaserAim", transform.position);
            ray.startColor = color;
            ray.endColor = color;
            aiming = true;
            timeAiming = 0f;
            eye.gameObject.GetComponent<LineRenderer>().enabled = true;
            ray.startWidth = ((Shadow)stateMachine).AimingRayWidth;
            ray.endWidth = ((Shadow)stateMachine).AimingRayWidth;

            target = playerChaser.transform;
            playerChaser.GetComponent<PlayerChaser>().velocity = ((Shadow)stateMachine).playerChaser_Speed;
            
        }

        ray.SetPosition(0, eye.transform.position);
        RaycastHit hit;
        Ray auxRay = new Ray(eye.transform.position, Vector3.Normalize((target.position + Vector3.up * 1f) - eye.transform.position));
        Physics.Raycast(auxRay, out hit);

        ray.SetPosition(1, hit.point);

        timeAiming += delta;

        if (timeAiming >= ((Shadow)stateMachine).AimingDuration)
        {
            aiming = false;
            return go.GetComponentInChildren<LoadShot>();
        }
        else return this;
        
        
        
    }

    public void setAiming(bool var)
    {
        aiming = var;
    }


}
