using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : State
{
    public State nextState;
    private ParticleSystem particles;
    private float explosionRange;
    private float distanceToDestroyThorns;

    private void Start()
    {
        distanceToDestroyThorns = ((Cube)stateMachine).distanceToDestroyThorns;
        explosionRange = ((Cube)stateMachine).explosionRange;
        particles = GetComponent<ParticleSystem>();
        var shape = particles.shape;
        shape.radius = explosionRange;
    }

    public override State tick(float delta)
    {
        particles.Play();
        if (Vector3.Distance(stateMachine.transform.position, stateMachine.player.transform.position) <= explosionRange) stateMachine.player.takeDamage();
        var zarzaList = FindObjectsOfType<Zarza>();
        foreach (Zarza zarza in zarzaList)
        {
            if (Vector3.Distance(stateMachine.transform.position, zarza.transform.position) < distanceToDestroyThorns) zarza.destroy();
        }
        return nextState.tick(delta);
    }
}
