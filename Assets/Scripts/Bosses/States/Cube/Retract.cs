using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retract : State
{
    public State nextState;
    public GameObject zarza;
    public GameObject zarzaGenerator;

    private bool mooving;
    private FMOD.Studio.EventInstance moveSound;
    private float timer;
    private float zarzaTimer;

    private void Start()
    {
        moveSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Cube/CubeMovement");
        zarzaTimer = ((Cube)stateMachine).zarzaSpawnTimer;
    }

    public override State tick(float delta)
    {
        if (!mooving)
        {
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(moveSound, stateMachine.gameObject.transform, stateMachine.GetComponent<Rigidbody>());
            moveSound.start();
            mooving = true;
        }
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
            stopSound();
            SoundHandler.playSound("event:/SFX/Cube/ImpactBetweenParts", transform.position);
            createZarza();
            return nextState;
        }
        return this;
    }

    private void createZarza()
    {
        Instantiate(zarza, zarzaGenerator.transform.position, zarzaGenerator.transform.rotation);
    }

    public void stopSound()
    {
        moveSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        mooving = false;
    }
}