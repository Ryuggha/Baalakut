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
    public int movementsToSweep;

    [Header("Shoot Attack")]
    public float AimingDuration;
    public float AimingRayWidth;
    public float ShotRayWidth;
    public float TimeBetweenAimingAndShots;
    public float ShotDuration;
    public float minDarknessLastingTime = 8;
    public float maxDarknessLastingTime = 12;
    public Color aimingColor;
    public Color shotColor;

    [Header("Sweep Attack")]
    public float angleToShot;
    public float sweepAcceleration;

    [Header("Vulneravility")]
    public int numberOfCrystals;
    public float stunTime = 2;
    private int activeCrystals = 0;
    public Material standardMaterial;
    public Material vulnerableMaterial;
    public Light vulnerableLight;
    public MeshRenderer eye;

    [Header("Trail Attack")]
    public float TrailDuration = 10; //Time in seconds



    public void MakeItVulnerable()
    {
        actualState = gameObject.GetComponentInChildren<Vulnerable>();
    }

    public override void hit()
    {
        if (activeCrystals >= numberOfCrystals)
        {
            var gas = FindObjectsOfType<SlowTrigger>();
            foreach (SlowTrigger s in gas)
            {
                s.stop();
            }
            die();
        }
    }

    public void addActiveCrystals(int i)
    {
        activeCrystals += i;
        
        if (activeCrystals >= numberOfCrystals)
        {
            eye.material = vulnerableMaterial;
            vulnerableLight.enabled = true;
            MakeItVulnerable();
        }
        else
        {
            eye.material = standardMaterial;
            vulnerableLight.enabled = false;
        }
    }
}
