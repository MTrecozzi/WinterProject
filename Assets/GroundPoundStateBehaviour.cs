using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// could literally be replaced with Constant Velocity State
[System.Serializable]
public class GroundPoundState : MovementState
{
    public float groundPoundVelocity = 0f;

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {

        {
            // Easing of this velocity, in partnership / communication with the pre pound buffer, would make this feel good
            currentVelocity = new Vector3(0, -groundPoundVelocity, 0);
        }

    }
}

[System.Serializable]
public class LagTransition : MovementState
{
    [SerializeField]
    public StateBuffer buffer;

    public override void Initialize()
    {
        buffer.SetStartTime();
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        currentVelocity = Vector3.zero;
    }

}

[System.Serializable]
public class ConstantVelocityState : MovementState
{
    public Vector2 velocityMagnitude;


    public override void Initialize()
    {
        base.Initialize();

        if (velocityMagnitude.y > 0)
        {
            controller.manager.Motor.ForceUnground();
        }
    }

    public override void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        return;
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        
        currentVelocity = controller.transform.forward.normalized *velocityMagnitude.x
            + Vector3.up * velocityMagnitude.y;
    }
}

[SerializeField]
public class GroundPoundStateBehaviour : MonoBehaviour
{
    
    public MTCharacterController controller;

    [Header("Movement States")]

    public GroundPoundState groundPoundState;

    [SerializeField]
    public LagTransition groundPoundLagState;

    [SerializeField]
    public ConstantVelocityState groundPoundCanceledJump;

    [SerializeField]
    public LagTransition groundPoundLandingLag;

    [SerializeField]
    public ConstantVelocityState groundPoundJump;

    //private MovementStateTransition GroundPoundInitialJump;

    private void Awake()
    {
        // this is really cool, the MovementState is its own logical unity, the behaviour manages it and can be used to 
        // compose its transitions without interference.

        Debug.LogWarning("Transition from groundPoundState to groundPoundLagState on grounded, instead of defaultState as is currently implemented");
        controller.manager.AddTransition(groundPoundState, IsGrounded, groundPoundLandingLag);

        // add transition from ground pound landing lag to normal if t > 0
        controller.manager.AddTransition(groundPoundLandingLag, GroundPoundTimedOut, controller.manager.defaultMoveState);

        // add transition from ground pound landing lag to constant vel groundPoundJump
        controller.manager.AddTransition(groundPoundLandingLag, JumpInputBuffered, groundPoundJump);
        controller.manager.AddTransition(groundPoundJump, FrameHasPassed, controller.manager.defaultMoveState);

        // Can't do this! as currently movement states need monobehaviour references set through the inspector
        Debug.LogWarning("Need to create a 'bind movement state to manager' system that populates the state with the appropriate controller, default move state" +
            ", and manager");
        //groundPoundCanceledJump = new ConstantVelocityState();

        // add a transition from initial lag into downward momentum, if we get jump input during groundPound
        controller.manager.AddTransition(groundPoundLagState, JumpCanceledInitialGroundPound, groundPoundCanceledJump);
        controller.manager.AddTransition(groundPoundLagState, InitialLagStateEnded, groundPoundState);

        // add transition from downward velocity to default state (to carry that velocity + control) after a frame has passed
        controller.manager.AddTransition(groundPoundCanceledJump, FrameHasPassed, controller.manager.defaultMoveState);
    }

    public bool InitialLagStateEnded()
    {
        bool validTransition = groundPoundLagState.buffer.StateEned();

        return validTransition;
    }

    public bool JumpInputBuffered()
    {
        var validTransition = controller.Jump.Buffered;

        if (validTransition)
        {
            controller.Jump.EatInput();
        }

        return validTransition;
    }

    public bool GroundPoundTimedOut()
    {
        return groundPoundLandingLag.buffer.StateEned();
    }

    public bool JumpCanceledInitialGroundPound()
    {
        bool validTransition = controller.Jump.Buffered;

        if (validTransition)
        {
            controller.Jump.EatInput();
        }

        return validTransition;
    }

    public bool FrameHasPassed()
    {
        return true;
    }

    public bool IsGrounded()
    {
        return controller.manager.Motor.GroundingStatus.FoundAnyGround;
    }

    public bool InitialGroundPoundJumpCheck()
    {
        return controller.Jump.Buffered && !groundPoundLagState.Motor.GroundingStatus.IsStableOnGround;
    }

    // this badddd, CENTRALIZE INPUT IDIOT
    private void FixedUpdate()
    {
        if (controller.controls.Standard.GroundPound.triggered)
        {

            Debug.LogWarning("Replace with a LagTransition of duration 0.13, that queues into ground pound unless interrupted by inputs");


            controller.manager.SetMovementState(groundPoundLagState);


            //groundPoundState.SetState();
        }
    }

}

[System.Serializable]
public class StateBuffer
{
    public float duration = 1f;
    [SerializeField]
    private float t;

    public void SetStartTime()
    {
        t = Time.time;
    }

    public bool StateEned()
    {
        return Time.time > t + duration;
    }
}
