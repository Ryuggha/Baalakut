using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private PlayerManager playerManager;
    private Transform cameraObject;
    private InputHandler inputHandler;
    [HideInInspector] public Vector3 moveDirection;

    private Vector3 normalVector;
    private Vector3 targetPosition;

    [HideInInspector] public Transform selfTransform;
    [HideInInspector] public AnimatorHandler animatorHandler;

    [HideInInspector] public new Rigidbody rigidbody;
    [HideInInspector] public GameObject normalCamera;

    [Header("Movement Stats")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float movementMultWhenCharging = 0.5f;
    [SerializeField] private float jumpSpeed = 1f;
    [SerializeField] private float minJumpTime = 1f;
    [SerializeField] private float maxJumpTime = 3f;
    [SerializeField] private float floatTimeProportion = 1f;
    [SerializeField] private float fallSpeed = 1f;
    [SerializeField] private float timeInAirToLandAnimation = 0.3f;

    [Header("Ground Detection")]
    [SerializeField] private float groundDetectionRayStartPoint = 0.5f;
    [SerializeField] private float minimumDistanceNeededToBeginFall = 1f;
    [SerializeField] private float groundDirectionRayDistance = 0.05f;
    private LayerMask layersForGroundCheck;

    [Header("Movement Flags")]
    public float inAirTimer;
    public float jumpTimer;


    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        cameraObject = Camera.main.transform;
        selfTransform = transform;
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
        animatorHandler.Initialize();
        playerManager.isGrounded = true;
        layersForGroundCheck = ~(1 << 8 | 1 << 11);
    }

    public void HandleMovement (float delta)
    {
        float velMultiplier = 1 - (playerManager.chargeStatus * movementMultWhenCharging);
        moveDirection = cameraObject.forward * inputHandler.vertical;
        moveDirection += cameraObject.right * inputHandler.horizontal;
        moveDirection.y = 0;

        float speed = movementSpeed;
        moveDirection *= speed * velMultiplier;

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        rigidbody.velocity = projectedVelocity;

        animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount * velMultiplier, 0);

        if (animatorHandler.canRotate)
        {
            HandleRotation(Time.deltaTime);
        }
    }

    private void HandleRotation (float delta)
    {
        if (inputHandler.rollFlag) return;
        if (playerManager.isInteracting) return;

        Vector3 targetDir = Vector3.zero;
        float moveOverride = inputHandler.moveAmount;

        targetDir = cameraObject.forward * inputHandler.vertical;
        targetDir += cameraObject.right * inputHandler.horizontal;

        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero) targetDir = selfTransform.forward;
        float rs = rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(selfTransform.rotation, tr, rs * delta);

        selfTransform.rotation = targetRotation;
    }

    public void HandleRolling (float delta)
    {
        if (animatorHandler.anim.GetBool("IsInteracting")) return;
        if (inputHandler.rollFlag)
        {
            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            if (inputHandler.moveAmount > 0.3f)
            {
                animatorHandler.PlayTargetAnimation("Rolling", true);
                moveDirection.y = 0;
                Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                selfTransform.rotation = rollRotation;
            }
        }
    }

    public void HandleJumping(float delta)
    {
        if (playerManager.isGrounded && inputHandler.jumpFlag && !playerManager.isJumping)
        {
            playerManager.isJumping = true;
            playerManager.isGrounded = false;
            playerManager.isAbleToJump = true;
            playerManager.isInteracting = true;
            this.jumpTimer = 0;
        }
        if (playerManager.isJumping)
        {
            if (playerManager.isAbleToJump && (jumpTimer < minJumpTime || inputHandler.jumpFlag))
            {
                rigidbody.AddForce(Vector3.up * jumpSpeed * delta * 10000);
                jumpTimer += delta;

                if (jumpTimer > maxJumpTime) playerManager.isAbleToJump = false;
            }
            else if (playerManager.isAbleToJump && !inputHandler.jumpFlag)
            {
                playerManager.isAbleToJump = false;
                floatTime(delta);
            }
            else if (!playerManager.isAbleToJump)
            {
                floatTime(delta);
                if (jumpTimer < 0) playerManager.isJumping = false;
            }
        }
    }

    private void floatTime(float delta)
    {
        this.jumpTimer -= delta / floatTimeProportion; 
    }

    public void HandleFalling (float delta, Vector3 moveDirection)
    {
        playerManager.isGrounded = false;
        RaycastHit hit;
        Vector3 origin = selfTransform.position;
        origin.y += groundDetectionRayStartPoint;
        if (Physics.Raycast(origin, selfTransform.forward, out hit, 0.4f))
        {
            moveDirection = Vector3.zero;
        }
        if (playerManager.isInAir)
        {
            if (!playerManager.isJumping) rigidbody.AddForce(-Vector3.up * fallSpeed * delta * 10000);
            rigidbody.AddForce(moveDirection * fallSpeed * delta * 1000);
        }

        Vector3 dir = moveDirection;
        dir.Normalize();
        origin += dir * groundDirectionRayDistance;

        targetPosition = selfTransform.position;

        Debug.DrawRay(origin, -Vector3.up * minimumDistanceNeededToBeginFall, Color.red, 0.1f, false);
        if (!playerManager.isJumping && Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, layersForGroundCheck))
        {
            normalVector = hit.normal;
            Vector3 tp = hit.point;
            playerManager.isGrounded = true;
            targetPosition.y = tp.y;

            if (playerManager.isInAir)
            {
                if (inAirTimer > timeInAirToLandAnimation)
                {
                    animatorHandler.PlayTargetAnimation("Land", true);
                }
                else
                {
                    animatorHandler.PlayTargetAnimation("Movement", false);
                }

                inAirTimer = 0;
                playerManager.isInAir = false;
            }
        }
        else
        {
            if (playerManager.isGrounded)
            {
                playerManager.isGrounded = false;
            }

            if (playerManager.isInAir == false)
            {
                if (playerManager.isInteracting == false)
                {
                    animatorHandler.PlayTargetAnimation("Falling", true);
                }

                Vector3 vel = rigidbody.velocity;
                vel.Normalize();
                rigidbody.velocity = vel * (movementSpeed / 2);
                playerManager.isInAir = true;
            }
        }

        if (playerManager.isGrounded)
        {
            if (playerManager.isInteracting || inputHandler.moveAmount > 0)
            {
                selfTransform.position = Vector3.Lerp(selfTransform.position, targetPosition, delta * 10);
            }
            else
            {
                selfTransform.position = targetPosition;
            }
        } 
    }
}
