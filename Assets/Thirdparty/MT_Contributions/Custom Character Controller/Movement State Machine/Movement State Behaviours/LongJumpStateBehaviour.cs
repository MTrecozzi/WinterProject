using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongJumpStateBehaviour : MoveStateBehaviour
{
    public MTCharacterController controller;

    public KinematicMotorAnimator anim;

    public LongJumpState longJumpState;


    public override MovementState[] GetManagedMoveStates()
    {
        // [ ! ] here we have a movement state managing a movement state, make sure to return the managed state so it can 
        // have its dependencies hooked up
        return new MovementState[] { longJumpState, longJumpState.bonkState };
    }

    private void Awake()
    {
        controller.manager.AddTransition(longJumpState, ControllerGrounded, controller.manager.defaultMoveStateBehaviour.defaultMoveState);
        controller.manager.AddTransition(longJumpState.bonkState, ControllerGrounded, controller.manager.defaultMoveStateBehaviour.defaultMoveState);
    }

    private void Start()
    {
        anim.animStateDefs.Add(longJumpState, "LongJump");
        anim.animStateDefs.Add(longJumpState.bonkState, "Bonk");
    }

    // [ ! ] Should also detect for wall collisions
    public bool ControllerGrounded()
    {
        return controller.manager.Motor.GroundingStatus.IsStableOnGround;
    }

}
