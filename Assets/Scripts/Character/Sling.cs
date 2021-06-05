using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sling : MonoBehaviour
{
    public Transform shotEmissor;
    public GameObject projectilePrefab;
    public ParticleSystem chargeParticles;

    [Header("ShotStats")]
    public float minSpeed = 10f;
    public float maxSpeed = 25f;
    public float minTimeCharging = .3f;
    public float maxTimeCharging = 1f;
    public float inertiaMultiplier = .5f;
    public float projectileGravityMultiplier;

    [Header("DebugValues")]
    [SerializeField] private float timeCharged;
    [SerializeField] private float velocityMultiplier;

    [Header("HUD")]
    [SerializeField] private int linePoints = 5;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    private bool shotFlag;
    private bool isCharging;

    private PlayerManager playerManager;
    private InputHandler inputHandler;
    private CameraHandler cameraHandler;
    private Rigidbody rb;
    private AnimatorHandler anim;
    private LineRenderer hud;

    private void Start()
    {
        anim = GetComponentInChildren<AnimatorHandler>();
        playerManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody>();
        cameraHandler = FindObjectOfType<CameraHandler>();
        inputHandler = GetComponent<InputHandler>();
        hud = GetComponentInChildren<LineRenderer>();
    }

    public float HandleShot(float delta) //returns a multiplier to modify the movement Speed
    {
        shotFlag = inputHandler.shotFlag;
        return handleChargeShot(delta);
    }

    private float handleChargeShot(float delta)
    {
        if (playerManager.isInteracting)
        {
            if (isCharging) stopCharging();
            timeCharged = 0;
            return 0f;
        }
        if (shotFlag)
        {
            if (!isCharging)
            {
                anim.setCharging(true);
                isCharging = true;
                chargeParticles.Play();
                SoundHandler.playSound("event:/SFX/Character/ProjectileCharge", transform.position);
            }
            timeCharged += delta;
            //RENDER HUD
            float shotModule = maxSpeed;
            if (timeCharged < maxTimeCharging)
                shotModule = minSpeed + ((maxSpeed - minSpeed) * ((timeCharged - minTimeCharging) / (maxTimeCharging - minTimeCharging)));
            Vector3 shotSpeed = cameraHandler.getCameraTransform().forward * shotModule;
            shotSpeed += rb.velocity * inertiaMultiplier;
            if (timeCharged >= minTimeCharging)
            {
                hud.enabled = true;
                renderHud(shotSpeed, projectileGravityMultiplier);
            }

            return Mathf.Clamp01(timeCharged / minTimeCharging);
        }
        else
        {
            if (isCharging)
            {
                stopCharging();
                shot();
                timeCharged = 0;
            }
            return 0f;
        }
    }

    private void stopCharging()
    {
        isCharging = false;
        anim.setCharging(false);
        hud.enabled = false;
        chargeParticles.Stop();
    }

    private void shot()
    {
        if (timeCharged >= minTimeCharging)
        {
            SoundHandler.playSound("event:/SFX/Character/ProjectileShot", transform.position);
            float shotModule = maxSpeed;
            if (timeCharged < maxTimeCharging)
                shotModule = minSpeed + ((maxSpeed - minSpeed) * ((timeCharged - minTimeCharging) / (maxTimeCharging - minTimeCharging)));
            Vector3 shotSpeed = cameraHandler.getCameraTransform().forward * shotModule;
            shotSpeed += rb.velocity * inertiaMultiplier;
            var projectile = Instantiate(projectilePrefab, shotEmissor.position, Quaternion.identity);
            SlingProjectile sling = projectile.GetComponent<SlingProjectile>();
            sling.setShotSpeed(shotSpeed);
            sling.setGravity(projectileGravityMultiplier);
        }
    }

    private void renderHud(Vector3 velocity, float gravityModifier)
    {
        Vector3[] array = calculatePoints(velocity, gravityModifier);
        hud.positionCount = array.Length;
        hud.SetPositions(array);
        colorSetting();
    }

    private Vector3[] calculatePoints(Vector3 velocity, float gravityModifier)
    {
        Vector3[] array = new Vector3[linePoints];
        Vector3 point;
        for (int i = 0; i<array.Length; i++) // i = tiempo en sec
        {
            point = shotEmissor.position + velocity * i * 0.05f + Physics.gravity * gravityModifier * Mathf.Pow(i*0.05f, 2) * 0.5f;

            Collider[] hitColliders = Physics.OverlapSphere(point, 0.1f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.layer == 12)
                {
                    Vector3[] subArray = new Vector3[i];
                    for (int j = 0; j < subArray.Length; j++)
                    {
                        subArray[j] = array[j];
                    }
                    return subArray;
                }
            }

            array[i] = point;
        }
        return array;
    }

    private void colorSetting() 
    {
        Color auxColor = Color.Lerp(startColor, endColor, (timeCharged - minTimeCharging) / (maxTimeCharging - minTimeCharging));
        hud.startColor = auxColor;
        hud.endColor = auxColor;
    }
}
