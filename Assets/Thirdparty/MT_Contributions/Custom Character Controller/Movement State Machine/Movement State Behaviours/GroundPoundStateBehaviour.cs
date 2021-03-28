using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class GroundPoundStateBehaviour : MoveStateBehaviour
{
    
    public MTCharacterController controller;

    public bool enableLongJump;

    [Header("Movement States")]

    public GroundPoundState groundPoundState;

    [SerializeField]
    public LagTransitionState initialLagState;

    [SerializeField]
    public ConstantVelocityState groundPoundCanceledJump;

    [SerializeField]
    public LagTransitionState groundPoundLandingLag;

    [SerializeField]
    public ConstantVelocityState groundPoundJump;

    public ScreenStateBehaviour screenStateBehaviour;

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


        controller.manager.AddTransition(groundPoundLandingLag, ScreenCollision, screenStateBehaviour.screenState);

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

    private bool ScreenCollision()
    {

        Debug.Log("SCREEN COLLISION CHECK");

        bool validTransition = false;

        RaycastHit closestHit;

        RaycastHit[] hits = new RaycastHit[2];

        var numbHits = controller.manager.Motor.CharacterCollisionsRaycast(transform.position, Vector3.down, 5f, out closestHit, hits, true);

        Debug.Log("Num Hits: " + numbHits);

        for (int i = 0; i < numbHits; i++)
        {

            Debug.Log("HIT " + hits[i].collider.gameObject.name);

            validTransition = ((screenStateBehaviour.screenLayers.value & (1 << hits[i].collider.gameObject.layer)) > 0);

            if (validTransition)
            {
                screenStateBehaviour.screenState.SetUp(hits[i].normal, true, 30f);
            }
        }

        return validTransition;
    }

    public override MovementState[] GetManagedMoveStates()
    {
        return new MovementState[] { groundPoundState, initialLagState, groundPoundCanceledJump, groundPoundLandingLag, groundPoundJump };
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
