using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : StateMachine
{
    public float launchVelocity;
    public float retractVelocity;
    public float offSetFixed;
    public float offSetPercentage;


    [Header("Modelos")]
    public GameObject front;
    public GameObject back;

    private float distanceToMove;
    [HideInInspector] public float offSet;


    public void setDistanceToMove(float distanceToMove)
    {
        offSet = distanceToMove * offSetPercentage + offSetFixed;
        this.distanceToMove = distanceToMove + offSet;
        
    }

    public float getDistanceToMove()
    {
        return this.distanceToMove;
    }
}
