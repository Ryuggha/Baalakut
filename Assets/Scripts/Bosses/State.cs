using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [HideInInspector] public GameObject go;
    [HideInInspector] public StateMachine stateMachine;

    public abstract State tick(float delta); //This must return the nextState
}
