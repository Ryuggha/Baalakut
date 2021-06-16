using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputHandler inputHandler;
    private Sling sling;
    private Animator anim;
    private AnimatorHandler animatorHandler;
    private CameraHandler cameraHandler;
    private Movement movement;
    private DarknessUI dUI;
    private InGameMenu menu;
    private RagdollController ragdoll;
    private SoundHandler sound;
    


    private bool dead;

    public float deathTimer = 3f;

    private bool darkened;
    private float darkenedTimer;

    [Header("Flags")]
    public bool isInteracting;
    public bool isGrounded;
    public bool isInAir;
    public float chargeStatus;
    public bool isAbleToJump;
    public bool isJumping;

    [Header("ShadowDarkness")]
    public float timeToDieFromDarkness;
    public float darknessSlow;

    [Header("Invincible time after boss kill")]
    public float timeInvincible = 2;
    private float timeLeft = 0;
    private bool invincible;

    private void Start()
    {
        sound = GetComponent<SoundHandler>();
        ragdoll = GetComponentInChildren<RagdollController>();
        menu = FindObjectOfType<InGameMenu>();
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
        dUI = FindObjectOfType<DarknessUI>();
        sling = GetComponent<Sling>();
        inputHandler = GetComponent<InputHandler>();
        anim = GetComponentInChildren<Animator>();
        movement = GetComponent<Movement>();
        cameraHandler = CameraHandler.singleton;
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        if (invincible)
        {
            timeLeft -= delta;
            if (timeLeft <= 0) invincible = false;
        }
        


        
            #region Handle Player State

            if (!menu.getPaused()) isInteracting = anim.GetBool("IsInteracting");

            #endregion

            #region Update all the Inputs

            inputHandler.TickInput(delta, dead);

        #endregion

            #region Handle Player Movement and Actions

            if (!menu.getPaused())
            {
                movement.HandleDashing(delta);
                chargeStatus = sling.HandleShot(delta);
                movement.HandleMovement(delta);
                movement.HandleFalling(delta, movement.moveDirection);
                animatorHandler.postShot(delta);
            }

            #endregion
    }

    private void LateUpdate()
    {
        float delta = Time.deltaTime;

        if (!menu.getPaused())
        {

            #region ShadowDarkness

            if (dUI != null)
            {
                if (darkened)
                {
                    movement.setSpeedModifier(darknessSlow);
                    darkenedTimer += delta;
                }
                else
                {
                    movement.setSpeedModifier(1);
                    darkenedTimer -= delta;
                    if (darkenedTimer < 0) darkenedTimer = 0;
                }
                if (darkenedTimer > timeToDieFromDarkness) darkenedTimer = timeToDieFromDarkness;
                darkened = false;
                dUI.setImageAlpha(darkenedTimer / timeToDieFromDarkness);
            }
            #endregion

            #region Move the Camera
            if (cameraHandler != null)
            {
                cameraHandler.adjustPivotTransformPosition(delta);
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(inputHandler.mouseX, inputHandler.mouseY, inputHandler.rStickX * delta, inputHandler.rStickY * delta);
            }
            #endregion

            #region Handling Flags

            if (isInAir && !isJumping) movement.inAirTimer += delta;

            #endregion

        }
    }

    public void startDash()
    {
        cameraHandler.setDashFollowSpeed();
    }

    public void endDash()
    {
        cameraHandler.resetFollowSpeed();
    }

    public void makePlayerInvincible()
    {
        timeLeft = timeInvincible;
        invincible = true;
    }

    public void takeDamage() //Unimplemented method
    {
        if (!dead && !invincible)
        {
            dead = true;
            ragdoll.ragdollEnabled(true);
            menu.die();
            SoundHandler.playSound("event:/SFX/Character/CharacterDeath", transform.position);
            Invoke("restartScene", deathTimer);
        }
    }

    public void instaKill()
    {
        if (!dead)
        {
            dead = true;
            ragdoll.ragdollEnabled(true);
            menu.die();
            SoundHandler.playSound("event:/SFX/Character/CharacterDeath", transform.position);
            Invoke("restartScene", deathTimer);
        }
    }

    public void restartScene()
    {
        FindObjectOfType<LevelLoader>().loadLevel(-1);
    }

    public void darknessTrigger()
    {
        darkened = true;
    }
}
