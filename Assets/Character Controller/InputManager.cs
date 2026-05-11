using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerInput PlayerInput;


    public static Vector2 Movement;
    public static bool JumpWasPressed;
    public static bool JumpIsHeld;
    public static bool JumpWasReleased;
    public static bool RunIsHeld;

    public static bool CamWasPressed;
    public static bool SnapWasPressed;
   

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _runAction;

    private InputAction _camAction;
    private InputAction _snapAction;


    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();

        _moveAction = PlayerInput.actions["Movement"];
        _jumpAction = PlayerInput.actions["Jump"];
        _runAction = PlayerInput.actions["Run"];
        //Adding new input to toggle lens mode
        _camAction = PlayerInput.actions["EnableCam"];
        _snapAction = PlayerInput.actions["Snap"];
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
        JumpWasPressed = _jumpAction.WasPressedThisFrame();
        JumpIsHeld = _jumpAction.IsPressed();
        JumpWasReleased = _jumpAction.WasReleasedThisFrame();
        RunIsHeld = _runAction.IsPressed();
        

        CamWasPressed = _camAction.IsPressed();
        SnapWasPressed = _snapAction.IsPressed();
    }
}
