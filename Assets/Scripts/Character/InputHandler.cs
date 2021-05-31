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

    public bool dashInput;
    [HideInInspector] public bool dashFlag;
    public bool shotInput;
    [HideInInspector] public bool shotFlag;
    public bool menuInput;
    [HideInInspector] public bool menuFlag;
    public bool submitInput;
    [HideInInspector] public bool submitFlag;

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
        HandleDashInput();
        HandleShotInput();
        HandleMenuInput();
        HandleSubmitInput();
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

    private void HandleDashInput()
    {
        dashInput = inputActions.PlayerActions.Dash.phase == UnityEngine.InputSystem.InputActionPhase.Started;
        dashFlag = dashInput;
    }

    private void HandleShotInput()
    {
        shotInput = inputActions.PlayerActions.Shot.phase == UnityEngine.InputSystem.InputActionPhase.Started;
        shotFlag = shotInput;
    }

    private void HandleMenuInput()
    {
        bool aux = menuInput;
        menuInput = inputActions.PlayerActions.Menu.phase == UnityEngine.InputSystem.InputActionPhase.Started;
        if (aux)
        {
            menuFlag = false;
        }
        else
        {
            menuFlag = menuInput;
        }
        
    }

    private void HandleSubmitInput()
    {
        bool aux = submitInput;
        submitInput = inputActions.PlayerActions.Submit.phase == UnityEngine.InputSystem.InputActionPhase.Started;
        if (aux)
        {
            submitFlag = false;
        }
        else
        {
            submitFlag = submitInput;
        }
    }
}