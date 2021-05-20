using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retract : State
{
    public State nextState;
    public GameObject zarza;
    public GameObject zarzaGenerator;

    private float timer;
    private float zarzaTimer;

    private void Start()
    {
        zarzaTimer = ((Cube)stateMachine).zarzaSpawnTimer;
    }

    public override State tick(float delta)
    {
        Vector3 aux = ((Cube)stateMachine).back.transform.localPosition;
        aux.z += ((Cube)stateMachine).retractVelocity * delta;
        bool reachedDestinacion = false;
        if (aux.z > 0)
        {
            aux.z = 0;
            reachedDestinacion = true;
        }
        ((Cube)stateMachine).back.transform.localPosition = aux;

        timer += delta;
        if (timer > zarzaTimer)
        {
            timer -= zarzaTimer;
            createZarza();
        }

        if (reachedDestinacion)
        {
            createZarza();
            return nextState;
        }
        return this;
    }

    private void createZarza()
    {
        Instantiate(zarza, zarzaGenerator.transform.position, zarzaGenerator.transform.rotation);
    }
}