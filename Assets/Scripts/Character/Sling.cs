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

    private bool shotFlag;
    private bool isCharging;

    private PlayerManager playerManager;
    private InputHandler inputHandler;
    private CameraHandler cameraHandler;
    private Rigidbody rb;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody>();
        cameraHandler = FindObjectOfType<CameraHandler>();
        inputHandler = GetComponent<InputHandler>();
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
            if (!isCharging) isCharging = true;
            timeCharged += delta;
            return Mathf.Clamp01(timeCharged / minTimeCharging);
        }
        else
        {
            if (isCharging)
            {
                isCharging = false;
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
}
