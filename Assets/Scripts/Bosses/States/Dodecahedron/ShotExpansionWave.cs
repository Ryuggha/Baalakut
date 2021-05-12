using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotExpansionWave : State
{
    public State nextState;
    public GameObject expansionWave;

    private FaceProtector[] protectors;
    private float timeToDissapear;
    private float startingRadius;
    private float expansionRate;
    private float deathArea;

    private void Start()
    {
        protectors = stateMachine.GetComponentsInChildren<FaceProtector>();
        Dodecaethron d = (Dodecaethron)stateMachine;
        timeToDissapear = d.timeToDissapear;
        startingRadius = d.startingRadius;
        expansionRate = d.expansionRate;
        deathArea = d.deathArea;
    }

    public override State tick(float delta)
    {
        bool shoot = false;

        if (Vector3.Angle(((Dodecaethron)stateMachine).GyroZ.transform.up, Vector3.up) < 0.3f) shoot = true;

        if (!shoot)
        {
            foreach (FaceProtector protector in protectors)
            {
                if (protector.getIsGrounded()) shoot = true;
                
            }
        }

        if (shoot) shootExpansionWave();

        return nextState.tick(delta);
    }

    public void shootExpansionWave()
    {
        ExpansionWave auxWave = Instantiate(expansionWave, gameObject.transform.position, Quaternion.identity).GetComponent<ExpansionWave>();
        auxWave.initialize(timeToDissapear, startingRadius, expansionRate, deathArea);
    }
}
