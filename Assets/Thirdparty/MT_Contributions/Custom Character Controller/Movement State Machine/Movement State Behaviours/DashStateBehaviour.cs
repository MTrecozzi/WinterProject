using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashStateBehaviour : MoveStateBehaviour
{

    public DashState dashState;

    public ScreenStateBehaviour screenStateBehaviour;

    public float customWallJumpCanceledDashEndMult = 0.7f;

    private void Awake()
    {
        MovementStateTransition wallJumpCanceledReorientedDash = new MovementStateTransition(dashState, WallJumpCanceledDash, dashState.defaultMoveState.defaultMoveState);

        MovementStateTransition dashEndedValidWallRun = new MovementStateTransition(dashState, DashStateEndedIntoValidWallRun, dashState.defaultMoveState.defaultMoveState);
        dashEndedValidWallRun.destinationHandled = true;

        MovementStateTransition dashEnded = new MovementStateTransition(dashState, DashStateEnded, dashState.defaultMoveState.defaultMoveState);

        MovementStateTransition screenState = new MovementStateTransition(dashState, ScreenCollision, screenStateBehaviour.screenState);

        Debug.LogWarning("Order of transitions matter! Note how timeEndedWallRun must come before timeEnded(generic)");
        dashState.controller.manager.AddTransition(wallJumpCanceledReorientedDash);
        //dashState.controller.manager.AddTransition(dashEndedValidWallRun);
        dashState.controller.manager.AddTransition(dashEnded);
        dashState.controller.manager.AddTransition(screenState);
    }

    public bool WallJumpCanceledDash()
    {
        bool validTransition = dashState.wallReoriented && dashState.currentWallNormal != Vector3.zero && dashState.controller.Jump.Buffered;

        if (validTransition)
        {
            dashState.controller.Jump.EatInput();

            // end dash removed, all it used to do was set state to default + invoke a UX event

            dashState.defaultMoveState.DampenAirAccel();

            // dash velocity = current movementum, halted less then usual
            dashState.dashVelocity *= (customWallJumpCanceledDashEndMult);

            // add to dash velocity a force from the normal of the wall
            dashState.dashVelocity += dashState.currentWallNormal.normalized * 10;

            // add a jump to the wall jump
            dashState.dashVelocity.y += dashState.defaultMoveState.defaultMoveState.JumpUpSpeed;

            Debug.Log("Current Wall Normal: " + dashState.currentWallNormal);

            // currentVelocity = dashVelocity + wall Jump
            dashState.defaultMoveState.Motor.BaseVelocity = dashState.dashVelocity;

        }

        // if true, we'll be initialized and the state machine will simply set us to the default move state, rather than us doing that ourselves,
        // other than that change, we've reached parity.
        return validTransition;
    }

    public bool DashStateEndedIntoValidWallRun()
    {
        Debug.LogWarning("Should Use a Bool property instead that read only returns whether t > time to reach");
        bool validTransition = dashState.t >= dashState.timeToReach && dashState.wallReoriented
            && dashState.initialDotProduct < dashState.dotProductBonkCutOff;

        if (validTransition)
        {
            Debug.Log("WALL ORIENTED: " + dashState.wallReoriented);

            dashState.wallRunState.StartState(dashState.surfaceParrallel, dashState.currentWallNormal);
        }

        return validTransition;
    }

    public bool ScreenCollision()
    {
        bool validTransition = false;

        validTransition = dashState.wallReoriented && ((screenStateBehaviour.screenLayers.value & (1 << dashState._collLayer)) > 0)
            || dashState.controller.manager.Motor.GroundingStatus.IsStableOnGround == true;

        if (validTransition)
        {
            screenStateBehaviour.screenState.SetUp(dashState.currentWallNormal);
        }

        return validTransition;
    }

    public bool DashStateEnded()
    {
        // set the valid transition equal to our conditional checks

        Debug.LogWarning("Use a dash expired property instead");
        bool validTransition = dashState.t >= dashState.timeToReach;

        if (validTransition) // if our transition is valid, make any necessary on exit initializations
        {
            dashState.dashVelocity = dashState.controller.manager.Motor.BaseVelocity * dashState.dashEndMultiplier;

            dashState.controller.manager.Motor.BaseVelocity = dashState.dashVelocity;
        }

        // alert the Move State Manager whether we're transitioning
        return validTransition;
    }



    private bool dummyCheck()
    {
        // set the valid transition equal to our conditional checks
        bool validTransition = false;

        if (validTransition) // if our transition is valid, make any necessary on exit initializations
        {

        }

        // alert the Move State Manager whether we're transitioning
        return validTransition;
    }

    public override MovementState[] GetManagedMoveStates()
    {
        return new MovementState[] { dashState };
    }
}
