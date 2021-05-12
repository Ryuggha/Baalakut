using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpansionWave : MonoBehaviour
{
    private ParticleSystem particles;
    private ParticleSystem.ShapeModule shape;
    private PlayerManager player;
    private float radius;
    private float timer;

    public float timeToDissapear;
    public float startingRadius;
    public float expansionRate;
    public float deathArea;

    private void Awake()
    {
        player = FindObjectOfType<PlayerManager>();
        particles = GetComponent<ParticleSystem>();
        shape = particles.shape;
        radius = startingRadius;
        timer = timeToDissapear;
    }

    public void initialize(float timeToDissapear, float startingRadius, float expansionRate, float deathArea)
    {
        this.timeToDissapear = timeToDissapear;
        this.startingRadius = startingRadius;
        this.expansionRate = expansionRate;
        this.deathArea = deathArea;
        tick();
        particles.Play();
    }

    void Update()
    {
        float delta = Time.deltaTime;
        timer -= delta;
        if (timer < 0)
        {
            particles.Stop();
            Destroy(gameObject, 5);
        }
        radius += delta * expansionRate;
        
        tick();
    }

    private void tick()
    {
        shape.radius = radius;
        float playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (Mathf.Abs(playerDistance - radius) < deathArea) player.takeDamage();
    }
}
