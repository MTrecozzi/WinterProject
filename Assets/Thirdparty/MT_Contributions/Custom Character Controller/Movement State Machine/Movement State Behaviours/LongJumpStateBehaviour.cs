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
        return new MovementState[] { longJumpState };
    }

    private void Awake()
    {
        controller.manager.AddTransition(longJumpState, ControllerGrounded, controller.manager.defaultMoveStateBehaviour.defaultMoveState);
    }

    private void Start()
    {
        anim.animStateDefs.Add(longJumpState, "LongJump");
    }

    // [ ! ] Should also detect for wall collisions
    public bool ControllerGrounded()
    {
        return controller.manager.Motor.GroundingStatus.IsStableOnGround;
    }

}
