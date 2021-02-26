using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunState : MovementState
{

    public float wallRunInitialVelocity = 16f;
    public float minWallRunVelocity = 12f;

    public float wallRunDecayRate = 6f;

    private float curYVelocity;

    public float InitialYVelocity = 2f;

    public float gravity;

    [SerializeField]
    private float curWallRunVelocityX;

    [SerializeField]
    private Vector3 surfaceParralel;

    private Vector3 surfaceNormal;

    public LayerMask mask;

    public void StartState(Vector3 _surfaceParralel, Vector3 _surfaceNormal)
    {
        surfaceParralel = _surfaceParralel;
        surfaceNormal = _surfaceNormal;

        controller.SetMovementState(this);
    }

    public override void Initialize()
    {


        Debug.Log("WALL RUN STATE INITIALIZED");

        curWallRunVelocityX = wallRunInitialVelocity;

        curYVelocity = InitialYVelocity;

        Debug.LogWarning("Wall Run State should set player orientation overriden to forward along wall normal.");

        //controller.OrientationMethod = OrientationMethod.TowardsMovement;



    }

    public override void CleanUp()
    {
        curWallRunVelocityX = 0f;

        Debug.Log("Wall Run Surface Ended");
        //surfaceNormal = Vector3.zero;

        curYVelocity = 0f;
    }

    // This is good logic, end states differently based on buffered inputs
    public void EndState()
    {
        

        if (controller.player.Jump.Buffered)
        {

            Debug.LogWarning("INTERESTING: jumping before the wall run, during the smaller dash window, has greater effect of preserving momentum, cool!");
            var velocity = (surfaceParralel.normalized * curWallRunVelocityX) * 0.8f + surfaceNormal.normalized * 10f;

            // add a jump to the wall jump
            velocity.y += controller.JumpUpSpeed;

            Debug.LogWarning("Terrible Wall Run Implmenetation: WALL COLLISION / NORMAL / STATE Should BE CENTRALIZED");
            controller.Motor.BaseVelocity = velocity;

            controller.player.Jump.EatInput();

            controller.DampenAirAccel();

        } else
        {
            var nonJumpVelocity = (surfaceParralel.normalized * Mathf.Min(controller.MaxAirMoveSpeed + 2, curWallRunVelocityX) * 0.8f);

            controller.Motor.BaseVelocity = nonJumpVelocity;
        }


        controller.SetDefaultMovementState();
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {

        

        Debug.Log("Wall Run Update");

        var input = controller.player.controls.Standard;

        // idiot! Was setting velocity = wallRunInitial rather than wallRunCurrent
        currentVelocity = (surfaceParralel.normalized * curWallRunVelocityX); //  + (surfaceParralel.normalized * 0.5f * input.ControlStick.ReadValue<Vector2>().y * wallRunVelocity);

        currentVelocity.y = curYVelocity + Mathf.Min(0,  (input.ControlStick.ReadValue<Vector2>().y * 3f));

        curYVelocity -= Time.deltaTime * gravity * 2;

        if (curWallRunVelocityX > minWallRunVelocity)
        {
            curWallRunVelocityX -= Time.deltaTime * 6f;
        } else
        {
            EndState();
        }

        if (controller.Motor.GroundingStatus.IsStableOnGround)
        {
            EndState();
        }

        

        if (controller.player.Jump.Buffered)
        {
            EndState();
        }

        // could be using planar constraints for this, check out motor.HasPlanarConstraints and the motor.PlanarConstraint

        RaycastHit hit;

        if (!Physics.Raycast(transform.position, surfaceNormal * -1, out hit, 2, mask))
        {
            EndState();
        } else if (Physics.Raycast(transform.position, surfaceParralel, 2, mask))
        {
            // hit somethign while running

            EndState();
        }
       
        //else { Debug.Log("HIT " + hit.transform.gameObject.name); }
        
    }
}
