using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using KinematicCharacterController.Examples;
using UnityEngine.InputSystem;

public class MTExamplePlayer : MonoBehaviour
{
    public MTCharacterController Character;
    public MTCharacterCamera CharacterCamera;

    public PlayerControls controls;

    public BufferedAction Jump;

    private void Start()
    {

        controls = new PlayerControls();
        controls.Enable();

        Jump = new BufferedAction();

        Cursor.lockState = CursorLockMode.Locked;

        // Tell camera to follow transform
        CharacterCamera.SetFollowTransform(Character.CameraFollowPoint);

        // Ignore the character's collider(s) for camera obstruction checks
        CharacterCamera.IgnoredColliders.Clear();
        CharacterCamera.IgnoredColliders.AddRange(Character.GetComponentsInChildren<Collider>());
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

    private void HandleBuffers()
    {
        if (controls.Standard.Jump.triggered) Jump.CallInput();

        Jump.Tick();

    }

    private void LateUpdate()
    {
        // Handle rotating the camera along with physics movers
        if (CharacterCamera.RotateWithPhysicsMover && Character.Motor.AttachedRigidbody != null)
        {
            CharacterCamera.PlanarDirection = Character.Motor.AttachedRigidbody.GetComponent<PhysicsMover>().RotationDeltaFromInterpolation * CharacterCamera.PlanarDirection;
            CharacterCamera.PlanarDirection = Vector3.ProjectOnPlane(CharacterCamera.PlanarDirection, Character.Motor.CharacterUp).normalized;
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
        Character.SetInputs(ref characterInputs);
    }
}
