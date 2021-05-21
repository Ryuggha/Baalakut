using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : StateMachine
{
    [Header("Movement")]
    public float SlowMovementDistance;
    public float FastMovementDistance;
    public float SlowMovementDuration;
    public float FastMovementDuration;
    public float SlowWalkChance;

    [Header("Shoot Attack")]
    public float AimingDuration;
    public float AimingRayWidth;
    public float ShotRayWidth;
    public float TimeBetweenAimingAndShots;
    public float ShotDuration;

    [Header("Vulneravility")]
    public int numberOfCrystals;
    public float stunTime = 2;
    private int activeCrystals = 0;

    [Header("Trail Attack")]
    public float TrailDuration = 10; //Time in seconds



    public void MakeItVulnerable()
    {
        actualState = gameObject.GetComponentInChildren<Vulnerable>();
    }

    public override void hit()
    {
        if (activeCrystals >= numberOfCrystals) die();
    }

    public void addActiveCrystals(int i)
    {
        activeCrystals += 1;
        
        if (activeCrystals >= numberOfCrystals)
        {
            MakeItVulnerable();
        }
    }
}
