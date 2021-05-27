using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunWait : State
{
    public State nextState;
    public float timeToWait;

    private ParticleSystem particles;
    private float timeLeft;
    private bool active = false;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    public override State tick(float delta)
    {
        if (!active)
        {
            particles.Play();
            active = true;
            timeLeft = timeToWait;
        }

        timeLeft -= delta;

        if (timeLeft <= 0)
        {
            particles.Stop();
            active = false;
            return nextState.tick(delta);
        }
        return this;
    }
}
