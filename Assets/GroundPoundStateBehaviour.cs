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

    public bool enableLongJump;

    [Header("Movement States")]

    public GroundPoundState groundPoundState;

    [SerializeField]
    public LagTransition initialLagState;

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

        // go from default move state to groundpound initial lag
        controller.manager.AddTransition(controller.manager.defaultMoveStateBehaviour.defaultMoveState, GroundPoundInitiateCheck, initialLagState);

        Debug.LogWarning("Transition from groundPoundState to groundPoundLagState on grounded, instead of defaultState as is currently implemented");
        controller.manager.AddTransition(groundPoundState, IsGrounded, groundPoundLandingLag);

        // add transition from ground pound landing lag to normal if t > 0
        controller.manager.AddTransition(groundPoundLandingLag, GroundPoundTimedOut, controller.manager.defaultMoveStateBehaviour.defaultMoveState);

        // add transition from ground pound landing lag to constant vel groundPoundJump
        controller.manager.AddTransition(groundPoundLandingLag, JumpInputBuffered, groundPoundJump);
        controller.manager.AddTransition(groundPoundJump, FrameHasPassed, controller.manager.defaultMoveStateBehaviour.defaultMoveState);

        // Can't do this! as currently movement states need monobehaviour references set through the inspector
        Debug.LogWarning("Need to create a 'bind movement state to manager' system that populates the state with the appropriate controller, default move state" +
            ", and manager");
        //groundPoundCanceledJump = new ConstantVelocityState();

        
        

        if (enableLongJump)
        {
            controller.manager.AddTransition(initialLagState, JumpCanceledInitialGroundPound, groundPoundCanceledJump);
        }

        // add a transition from initial lag into downward momentum, if we get jump input during groundPound
        controller.manager.AddTransition(initialLagState, InitialLagStateEnded, groundPoundState);
        

        // add transition from downward velocity to default state (to carry that velocity + control) after a frame has passed
        controller.manager.AddTransition(groundPoundCanceledJump, FrameHasPassed, controller.manager.defaultMoveStateBehaviour.defaultMoveState);
    }

    public bool InitialLagStateEnded()
    {
        bool validTransition = initialLagState.buffer.StateEned();

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
        return controller.Jump.Buffered && !initialLagState.Motor.GroundingStatus.IsStableOnGround;
    }

    public bool GroundPoundInitiateCheck()
    {
        var valid = controller.controls.Standard.GroundPound.triggered && !controller.manager.Motor.GroundingStatus.FoundAnyGround;
        return valid;
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
