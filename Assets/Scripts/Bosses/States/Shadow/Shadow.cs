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
    public GameObject playerChaser;
    public float playerChaser_Speed;

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
    public MeshRenderer[] eye;

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
            SoundHandler.playSound("event:/SFX/Shadow/ShadowDeath", transform.position);
            die();
        }
    }

    public void addActiveCrystals(int i)
    {
        activeCrystals += i;

        if (activeCrystals >= numberOfCrystals)
        {
            foreach (var mesh in eye)
            {   
                if(mesh != null)
                    mesh.material = vulnerableMaterial;
            }
            if(vulnerableLight != null) {
                vulnerableLight.enabled = true;
                MakeItVulnerable();
                SoundHandler.playSound("event:/SFX/Shadow/ShadowVulnerable", transform.position);
            }
        }
        else
        {
            foreach (var mesh in eye)
            {
                if(mesh != null)
                    mesh.material = standardMaterial;
            }
            if(vulnerableLight != null)
                vulnerableLight.enabled = false;
        }
    }

    public override void die()
    {
        GameData gd = SaveSystem.LoadGame();
        gd.shadowKilled = true;
        SaveSystem.saveGame(gd);
        base.die();
    }
}
