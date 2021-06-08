using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornAttackWait : State
{
    public State nextState;
    public float timeToWait;

    private ThornGeneratorParticles particles;

    private float timeLeft;
    private bool active = false;

    private void Start()
    {
        particles = FindObjectOfType<ThornGeneratorParticles>();
    }

    public override State tick(float delta)
    {
        if (!active)
        {
            stateMachine.GetComponentInChildren<ThornAttack>().startSound();
            particles.transform.position = go.transform.position;
            particles.GetComponent<ParticleSystem>().Play();
            active = true;
            timeLeft = timeToWait;
        }

        timeLeft -= delta;

        if (timeLeft <= 0)
        {
            active = false;
            return nextState.tick(delta);
        }
        return this;
    }
}
