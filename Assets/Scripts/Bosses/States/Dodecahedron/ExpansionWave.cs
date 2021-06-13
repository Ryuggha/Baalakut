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
    private bool isDestroyed;
    private FMOD.Studio.EventInstance waveSound;

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

    public void initialize(float timeToDissapear, float startingRadius, float expansionRateMin, float expansionRateMax, float deathArea)
    {
        this.timeToDissapear = timeToDissapear;
	    timer = timeToDissapear;
        this.startingRadius = startingRadius;
        this.expansionRate = Random.Range(expansionRateMin, expansionRateMax);
        this.deathArea = deathArea;
        tick();
        particles.Play();
        SoundHandler.playSound("event:/SFX/Dodecahedron/CreateWave", transform.position);
        waveSound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Dodecahedron/WaveSound");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(waveSound, transform, new Rigidbody());
        waveSound.start();
    }

    void Update()
    {
        float delta = Time.deltaTime;
        timer -= delta;
        if (timer < 0)
        {
            this.destroy();
        }
        radius += delta * expansionRate;
        if (!isDestroyed ) tick();
    }

    private void tick()
    {
        shape.radius = radius;
        float playerDistance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if (Mathf.Abs(playerDistance - radius) < deathArea) player.takeDamage();
    }

    public void destroy()
    {
        isDestroyed = true;
        particles.Stop();
        Destroy(gameObject, 3);
    }

    private void OnDestroy()
    {
        waveSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
