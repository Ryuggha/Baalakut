using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : StateMachine
{
    public float launchVelocity;
    public float retractVelocity;
    public float offSetFixed;
    public float offSetPercentage;
    public float stunTime;
    public float explosionRange;
    public float zarzaTimer = 0.05f;
    public int nTimesToExplote = 4;

    [Header("Modelos")]
    public GameObject front;
    public GameObject back;

    private float distanceToMove;
    [HideInInspector] public float offSet;

    private bool stun;
    private float timer;


    public void setDistanceToMove(float distanceToMove, bool withOffsets)
    {
        offSet = distanceToMove * offSetPercentage + offSetFixed;
        if (!withOffsets) offSet = 0;
        this.distanceToMove = distanceToMove + offSet;
        
    }

    public float getDistanceToMove()
    {
        return this.distanceToMove;
    }

    protected override void Update()
    {
        if (stun)
        {
            if (timer == 0) timer = stunTime;
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 0;
                stun = false;
            }
        }
        else base.Update();
    }

    public void stunned()
    {
        this.stun = true;
        var aux = GetComponentInChildren<TimesLaunched>();
        aux.counter = 0;
    }
}
