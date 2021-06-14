using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornAttack : State
{
    public State nextState;
    public GameObject Thorn;
    private ThornGeneratorParticles particles;
    private float durationOfAttack;
    private float periodOfThornAttackSpawn;
    private FMOD.Studio.EventInstance attackSound;

    private float globalTimer;
    private float periodTimer;

    private void Start()
    {
        attackSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Cube/ThornAttack");
        particles = FindObjectOfType<ThornGeneratorParticles>();
        durationOfAttack = ((Cube)stateMachine).durationOfAttack;
        periodOfThornAttackSpawn = ((Cube)stateMachine).periodOfThornAttackSpawn;
        globalTimer = durationOfAttack;
    }

    public override State tick(float delta)
    {
        globalTimer -= delta;
        periodTimer -= delta;

        if (periodTimer <= 0)
        {
            periodTimer = periodOfThornAttackSpawn;
            Vector3 target = stateMachine.player.gameObject.transform.position;
            target.y = 0;
            Instantiate(Thorn, target, Quaternion.identity);
        }

        if (globalTimer <= 0) 
        {
            attackSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            particles.GetComponent<ParticleSystem>().Stop();
            globalTimer = durationOfAttack;
            return nextState.tick(delta);
        }

        return this;
    }

    public void startSound()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(attackSound, stateMachine.gameObject.transform, stateMachine.GetComponent<Rigidbody>());
        attackSound.start();
    }

    private void OnDestroy()
    {
        attackSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}

