using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Camera Options")]
    [SerializeField] [Range(0, 5)] private float mouseCameraSensibility = 1f;
    [SerializeField] [Range(0, 5)] private float controllerCameraSensibility = 1f;
    [HideInInspector] public float fov = 60;

    [Header("Debug flags")]
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX, mouseY, rStickX, rStickY;

    public bool rollInput;
    [HideInInspector] public bool rollFlag;
    public bool jumpInput;
    [HideInInspector] public bool jumpFlag;
    public bool shotInput;
    [HideInInspector] public bool shotFlag;
    public bool menuInput;
    [HideInInspector] public bool menuFlag;

    PlayerControls inputActions;
    CameraHandler cameraHandler;

    Vector2 movementInput, cameraInput, cameraMouseInput;

    public void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            inputActions.PlayerMovement.CameraMouse.performed += j => cameraMouseInput = j.ReadValue<Vector2>();
        }
        inputActions.Enable();   
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        MoveInput();
        HandleRollInput();
        HandleShotInput();
        HandleJumpInput();
        HandleMenuInput();
    }

    private void MoveInput()
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        mouseX = cameraMouseInput.x / 20f * mouseCameraSensibility;
        mouseY = cameraMouseInput.y / 20f * mouseCameraSensibility;
        rStickX = cameraInput.x * controllerCameraSensibility;
        rStickY = cameraInput.y * controllerCameraSensibility;
    }

    private void HandleRollInput()
    {
        rollInput = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
        if (rollInput) rollFlag = true;
    }

    private void HandleJumpInput()
    {
        jumpInput = inputActions.PlayerActions.Jump.phase == UnityEngine.InputSystem.InputActionPhase.Started;
        jumpFlag = jumpInput;
    }

    private void HandleShotInput()
    {
        shotInput = inputActions.PlayerActions.Shot.phase == UnityEngine.InputSystem.InputActionPhase.Started;
        shotFlag = shotInput;
    }

    private void HandleMenuInput()
    {
        menuInput = inputActions.PlayerActions.Menu.phase == UnityEngine.InputSystem.InputActionPhase.Started;
        menuFlag = menuInput;
    }
}