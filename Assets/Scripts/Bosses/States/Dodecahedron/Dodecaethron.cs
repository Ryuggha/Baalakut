using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodecaethron : StateMachine
{
    [Header("MovementStats")]
    public float optimalMovementProbability = 0.7f;
    public float timeMoving = 1;
    public float expantionWaveVel = 1;

    [Header("ExpansionWaveStats")]
    public float timeToDissapear;
    public float startingRadius;
    public float expansionRateMin;
    public float expansionRateMax;
    public float deathArea;

    [HideInInspector]public Vector3 targetPos;

    [Header("Modelos")]
    public GameObject GyroX;
    public GameObject GyroY;
    public GameObject GyroZ;

    [Header("No toques esto si no sabes la fórmula para calcularlo")]
    public float distanceToMove;

    private bool weakState;

    public override void hit()
    {
        if (weakState)
        {
            var aux = FindObjectsOfType<ExpansionWave>();
            foreach (ExpansionWave ew in aux) ew.destroy();
            base.hit();
            die();
            SoundHandler.playSound("event:/SFX/Dodecahedron/DodecahedronDeath", transform.position);
        }
    }

    public void setWeakState()
    {
        Invoke("setWeakStateDelayed", 0.3f);
    }

    private void setWeakStateDelayed()
    {
        weakState = true;
    }
}
