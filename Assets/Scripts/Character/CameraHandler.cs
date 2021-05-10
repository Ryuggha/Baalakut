using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform targetTransform;
    public Transform cameraTransform;
    public Transform cameraPivotTransform;
    public Transform cameraFreeLookPivotPos;
    public Transform cameraChargingPivotPos;
    private Transform selfTransform;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Camera cam;

    private Vector3 cameraTransformPosition;
    [SerializeField] private LayerMask layerMask;
    [HideInInspector] public static CameraHandler singleton;

    public float fovMultiplicatorWhileCharging = 0.8f;
    public float lookSpeed = 0.01f, followSpeed = 4, dashFollowSpeed = 0.01f, pivotSpeed = 0.007f;
    private float defaultPosition, lookAngle, pivotAngle, targetPosition;
    public float minimumPivot = -35f, maximumPivot = 35f;
    private float followSpeedRaw;
    private PlayerManager playerManager;
    private InputHandler inputManager;

    public float cameraSphereRadius = 0.2f, cameraCollisionOffSet = 0.2f, minimumCollisionOffset = 0.2f;

    private void Awake()
    {
        followSpeedRaw = followSpeed;
        cam = GetComponentInChildren<Camera>();
        inputManager = FindObjectOfType<InputHandler>();
        playerManager = FindObjectOfType<PlayerManager>();
        singleton = this;
        selfTransform = transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void adjustPivotTransformPosition(float delta)
    {
        if (playerManager.chargeStatus > 0)
        {
            cameraPivotTransform.position = Vector3.Lerp(cameraPivotTransform.position, cameraChargingPivotPos.position, delta);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, inputManager.fov * fovMultiplicatorWhileCharging, delta);
        }
        else
        {
            cameraPivotTransform.position = Vector3.Lerp(cameraPivotTransform.position, cameraFreeLookPivotPos.position, delta * 5);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, inputManager.fov, delta * 5);
        }
    }

    public void setDashFollowSpeed()
    {
        this.followSpeedRaw = dashFollowSpeed;
    }

    public void resetFollowSpeed()
    {
        this.followSpeedRaw = followSpeed;
    }

    public void FollowTarget(float delta)
    {
        Vector3 targetPosition = Vector3.SmoothDamp(selfTransform.position, targetTransform.position, ref cameraFollowVelocity, followSpeedRaw);
        selfTransform.position = targetPosition;

        HandleCameraCollisions(delta);
    }

    public void HandleCameraRotation(float mouseXInput, float mouseYInput, float stickXInput, float stickYInput)
    {
        lookAngle += ((mouseXInput + stickXInput) * lookSpeed * 10);
        pivotAngle -= ((mouseYInput + stickYInput) * pivotSpeed * 10);
        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        selfTransform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;

        targetRotation = Quaternion.Euler(rotation);
        cameraPivotTransform.localRotation = targetRotation;
    }

    private void HandleCameraCollisions(float delta)
    {
        targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(targetPosition), layerMask))
        {
            float distance = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetPosition = -(distance - cameraCollisionOffSet);
        }
        if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
        {
            targetPosition = -minimumCollisionOffset;
        }

        cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, delta / 0.2f);
        cameraTransform.localPosition = cameraTransformPosition;        
    }

    public Transform getCameraTransform()
    {
        return this.cameraTransform;
    }
}
