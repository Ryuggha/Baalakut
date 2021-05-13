using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sling : MonoBehaviour
{
    public Transform shotEmissor;
    public GameObject projectilePrefab;

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
        hud = GetComponent<LineRenderer>();
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
            timeCharged = 0;
            return 0f;
        }
        if (shotFlag)
        {
            if (!isCharging)
            {
                anim.setCharging(true);
                isCharging = true;
            }
            timeCharged += delta;
            //RENDER HUD
            hud.enabled = true;
            float shotModule = maxSpeed;
            if (timeCharged < maxTimeCharging)
                shotModule = minSpeed + ((maxSpeed - minSpeed) * ((timeCharged - minTimeCharging) / (maxTimeCharging - minTimeCharging)));
            Vector3 shotSpeed = cameraHandler.getCameraTransform().forward * shotModule;
            shotSpeed += rb.velocity * inertiaMultiplier;
            renderHud(shotSpeed, projectileGravityMultiplier);

            return Mathf.Clamp01(timeCharged / minTimeCharging);
        }
        else
        {
            if (isCharging)
            {
                isCharging = false;
                anim.setCharging(false);
                hud.enabled = false;
                shot();
                timeCharged = 0;
            }
            return 0f;
        }
        
    }

    private void shot()
    {
        if (timeCharged >= minTimeCharging)
        {
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
        hud.positionCount = linePoints + 1;
        hud.SetPositions(calculatePoints(velocity, gravityModifier));
    }

    private Vector3[] calculatePoints(Vector3 velocity, float gravityModifier)
    {
        Debug.Log("Velocity: " + velocity);
        Debug.Log("Origin Point outsite: " + shotEmissor.position);
        Vector3[] array = new Vector3[linePoints+1];
        Vector3 point;
        //Vector3 origin = shotEmissor.position;
        for (int i = 0; i<array.Length; i++) // i = tiempo en sec
        {
            point = shotEmissor.position + velocity * i * 0.05f + Physics.gravity * gravityModifier * Mathf.Pow(i*0.05f, 2) * 0.5f; 
            Debug.Log("point "+ i +":" + point);
            array[i] = point;
        }
        return array;
    }
}
