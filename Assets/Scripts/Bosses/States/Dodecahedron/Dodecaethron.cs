using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodecaethron : StateMachine
{
    public float optimalMovementProbability = 0.7f;
    public float timeMoving = 1;
    public float explotionVel = 1;
     public Vector3 targetPos;

    [Header("Modelos")]
    public GameObject GyroX;
    public GameObject GyroY;
    public GameObject GyroZ;

    [Header("No toques esto si no sabes la f�rmula para calcularlo")]
    public float distanceToMove;
}