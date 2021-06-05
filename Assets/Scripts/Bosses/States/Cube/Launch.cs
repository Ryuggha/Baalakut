using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : State
{
    [HideInInspector] public bool gonnaStun;
    public State nextState;
    private bool mooving;
    private FMOD.Studio.EventInstance moveSound;

    private void Start()
    {
        moveSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Cube/CubeMovement");
        
    }

    public override State tick(float delta)
    {
        if (!mooving)
        {
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(moveSound, stateMachine.gameObject.transform, stateMachine.GetComponent<Rigidbody>());
            moveSound.start();
            mooving = true;
        }
        
        Vector3 aux = ((Cube)stateMachine).front.transform.localPosition;
        aux.z += ((Cube)stateMachine).launchVelocity * delta;
        float distanceToMove = ((Cube)stateMachine).getDistanceToMove();
        bool reachedDestinacion = false;
        if (aux.z > distanceToMove)
        {
            aux.z = distanceToMove;
            reachedDestinacion = true;
        }
        ((Cube)stateMachine).front.transform.localPosition = aux;

        if (reachedDestinacion)
        {
            stopSound();
            if (gonnaStun) ((Cube)stateMachine).stunned();
            gonnaStun = false;
            return nextState;
        }
        return this;
    }

    public void stopSound()
    {
        moveSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        mooving = false;
    }
}
