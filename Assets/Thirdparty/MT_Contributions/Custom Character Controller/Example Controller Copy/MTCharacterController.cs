using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using System;

// To Do, have a much cleaner reference to an input class rather than all these input bools, create functionality for input 'consumption', or just use better code

// this currently has 2 responsibilities that should be seperated; default movementstate, and CharacterController

public class MTCharacterController : MonoBehaviour
{
    
    public MTExamplePlayer player;

    public Transform CameraFollowPoint;

    public event Action OnPlayerJump;
    public event Action OnPlayerLanded;
    public event Action OnPlayerDoubleJump;

    public void SetMovementState(MovementState movementState) => manager.SetMovementState(movementState);
    public void SetDefaultMovementState() => manager.SetDefaultMovementState();

    public MovementState curMovementState => manager.curMovementState;

    public OrientationMethod OrientationMethod = OrientationMethod.TowardsCamera;

    public MoveStateManager manager;

    public void ForceUnground() => manager.Motor.ForceUnground();

    public MTCharacterCamera CharacterCamera;

    public PlayerControls controls;

    public BufferedAction Jump;

    // make get only, with private set
    public Vector3 MoveInput => _moveInputVector;
    public Vector3 LookInput => _lookInputVector;

    public void SetPropulsionForce(Vector3 vector3) => manager.SetPropulsionForce(vector3);
    

    private Vector3 _moveInputVector;
    private Vector3 _lookInputVector;

    public void DoubleJumpEvent()
    {
        OnPlayerDoubleJump?.Invoke();
    }

    public void LandedEvent()
    {
        OnPlayerLanded?.Invoke();
    }

    private void Start()
    {

        controls = new PlayerControls();
        controls.Enable();

        Jump = new BufferedAction();

        Cursor.lockState = CursorLockMode.Locked;

        // Tell camera to follow transform
        CharacterCamera.SetFollowTransform(CameraFollowPoint);

        // Ignore the character's collider(s) for camera obstruction checks
        CharacterCamera.IgnoredColliders.Clear();
        CharacterCamera.IgnoredColliders.AddRange(GetComponentsInChildren<Collider>());
    }

    private void FixedUpdate()
    {
        if (controls.Standard.Shoot.triggered)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        HandleBuffers();
        HandleCharacterInput();
    }

    public void MoveThePlayer(Vector3 pos)
    {
        manager.Motor.SetPosition(pos);
    }

    private void HandleBuffers()
    {
        if (controls.Standard.Jump.triggered) Jump.CallInput();

        Jump.Tick();

    }

    private void LateUpdate()
    {
        // Handle rotating the camera along with physics movers
        if (CharacterCamera.RotateWithPhysicsMover && manager.Motor.AttachedRigidbody != null)
        {
            CharacterCamera.PlanarDirection = manager.Motor.AttachedRigidbody.GetComponent<PhysicsMover>().RotationDeltaFromInterpolation * CharacterCamera.PlanarDirection;
            CharacterCamera.PlanarDirection = Vector3.ProjectOnPlane(CharacterCamera.PlanarDirection, manager.Motor.CharacterUp).normalized;
        }

        HandleCameraInput();
    }

    private void HandleCameraInput()
    {
        // Create the look input vector for the camera

        var delta = controls.Standard.AimDelta.ReadValue<Vector2>();

        delta *= 0.5f;

        delta *= 0.1f;

        float mouseLookAxisUp = delta.y;
        float mouseLookAxisRight = delta.x;


        Vector3 lookInputVector = new Vector3(mouseLookAxisRight, mouseLookAxisUp, 0f);

        // Prevent moving the camera while the cursor isn't locked
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            lookInputVector = Vector3.zero;
        }

        // Input for zooming the camera (disabled in WebGL because it can cause problems)
        float scrollInput = 0; // -Input.GetAxis(MouseScrollInput);
#if UNITY_WEBGL
    scrollInput = 0f;
#endif

        // Apply inputs to the camera
        CharacterCamera.UpdateWithInput(Time.deltaTime, scrollInput, lookInputVector);

    }

    private void HandleCharacterInput()
    {
        PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

        // Build the CharacterInputs struct
        characterInputs.MoveAxisForward = controls.Standard.ControlStick.ReadValue<Vector2>().y;
        characterInputs.MoveAxisRight = controls.Standard.ControlStick.ReadValue<Vector2>().x;
        characterInputs.CameraRotation = CharacterCamera.Transform.rotation;
        characterInputs.JumpDown = Jump.Buffered;

        // not utilizing jump up right now
        // Apply inputs to character
        SetInputs(ref characterInputs);
    }

    public void SetInputs(ref PlayerCharacterInputs inputs)
    {

        // Clamp input
        Vector3 moveInputVector = Vector3.ClampMagnitude(new Vector3(inputs.MoveAxisRight, 0f, inputs.MoveAxisForward), 1f);

        // Calculate camera direction and rotation on the character plane
        Vector3 cameraPlanarDirection = Vector3.ProjectOnPlane(inputs.CameraRotation * Vector3.forward, manager.Motor.CharacterUp).normalized;
        if (cameraPlanarDirection.sqrMagnitude == 0f)
        {
            cameraPlanarDirection = Vector3.ProjectOnPlane(inputs.CameraRotation * Vector3.up, manager.Motor.CharacterUp).normalized;
        }
        Quaternion cameraPlanarRotation = Quaternion.LookRotation(cameraPlanarDirection, manager.Motor.CharacterUp);


        // Move and look inputs
        _moveInputVector = cameraPlanarRotation * moveInputVector;

        switch (OrientationMethod)
        {
            case OrientationMethod.TowardsCamera:
                _lookInputVector = cameraPlanarDirection;
                break;
            case OrientationMethod.TowardsMovement:
                _lookInputVector = _moveInputVector.normalized;
                break;
        }

    }




}
