using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodecaethron : StateMachine
{
    [Header("MovementStats")]
    public float optimalMovementProbability = 0.7f;
    public float timeMoving = 1;
    public float expantionWaveVel = 1;
    public float minDistance;

    [Header("ExpansionWaveStats")]
    public float timeToDissapear;
    public float startingRadius;
    public float expansionRate;
    public float deathArea;

    [HideInInspector]public Vector3 targetPos;

    [Header("Modelos")]
    public GameObject GyroX;
    public GameObject GyroY;
    public GameObject GyroZ;

    [Header("No toques esto si no sabes la fórmula para calcularlo")]
    public float distanceToMove;
}
