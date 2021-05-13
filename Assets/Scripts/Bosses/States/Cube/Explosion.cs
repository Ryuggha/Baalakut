using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : State
{
    public State nextState;
    private ParticleSystem particles;
    private float explosionRange;

    private void Start()
    {
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
        foreach (Zarza zarza in zarzaList) zarza.destroy(); 
        return nextState.tick(delta);
    }
}
