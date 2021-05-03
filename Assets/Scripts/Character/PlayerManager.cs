using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private InputHandler inputHandler;
    private Sling sling;
    private Animator anim;
    private CameraHandler cameraHandler;
    private Movement movement;

    [Header("Flags")]
    public bool isInteracting;
    public bool isGrounded;
    public bool isInAir;
    public float chargeStatus;
    public bool isAbleToJump;
    public bool isJumping;

    private void Start()
    {
        sling = GetComponent<Sling>();
        inputHandler = GetComponent<InputHandler>();
        anim = GetComponentInChildren<Animator>();
        movement = GetComponent<Movement>();
        cameraHandler = CameraHandler.singleton;
    }

    private void Update()
    {
        float delta = Time.deltaTime;

        if (inputHandler.menuFlag) UnityEngine.SceneManagement.SceneManager.LoadScene(0);

        #region Handle Player State

        isInteracting = anim.GetBool("IsInteracting");

        #endregion

        #region Update all the Inputs

        inputHandler.TickInput(delta);

        #endregion

        #region Handle Player Movement and Actions

        chargeStatus = sling.HandleShot(delta);
        movement.HandleMovement(delta);
        movement.HandleRolling(delta);
        movement.HandleJumping(delta);
        movement.HandleFalling(delta, movement.moveDirection);

        #endregion
    }

    private void LateUpdate()
    {
        float delta = Time.deltaTime;

        #region Move the Camera
        if (cameraHandler != null)
        {
            cameraHandler.adjustPivotTransformPosition(delta);
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(inputHandler.mouseX, inputHandler.mouseY, inputHandler.rStickX * delta, inputHandler.rStickY * delta);
        }
        #endregion

        #region Handling Flags

        inputHandler.rollFlag = false;

        if (isInAir && !isJumping) movement.inAirTimer += delta;

        #endregion
    }

    public void takeDamage() //Unimplemented method
    {
        Debug.Log("Probably, this will simply kill the player, but for now, it only shows a Log");
    }
}