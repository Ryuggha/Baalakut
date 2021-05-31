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

    private void Start()
    {
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

        #region Handle Player State
        
        if (!menu.getPaused()) isInteracting = anim.GetBool("IsInteracting");

        #endregion

        #region Update all the Inputs

        inputHandler.TickInput(delta);

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

    public void takeDamage() //Unimplemented method
    {
        isInteracting = true;
        Invoke("restartScene", 0.2f);
        //Debug.Log("Probably, this will simply kill the player, but for now, it only shows a Log");
    }

    public void restartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void darknessTrigger()
    {
        darkened = true;
    }
}
