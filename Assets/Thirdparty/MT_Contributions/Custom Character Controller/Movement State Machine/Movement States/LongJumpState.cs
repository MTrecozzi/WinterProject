using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LongJumpState : MovementState
{
    [Header("Verticallity")]
    public float initialYVelocity = 0;
    public float maxFallSpeed = 10f;
    public Vector3 gravity;

    [Header("Horizontally")]
    public float launchSpeed;
    public float mainDirCancellationFactor = 20f;

    public float HorizontalDirAccelerationFactor = 15f;

    public Vector3 launchDir;
    public Vector3 velocity;


    private Quaternion startRot;
    


    public override void Initialize()
    {
        base.Initialize();

        launchDir = controller.manager.defaultMoveStateBehaviour.defaultMoveState.MeshRoot.transform.forward;

        launchDir.y = 0;

        velocity = launchDir.normalized * launchSpeed;

        velocity.y = initialYVelocity;

        startRot = controller.manager.Motor.TransientRotation;
    }

    public override void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        currentRotation = startRot;
    }

    public override void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
        if (!hitStabilityReport.IsStable && Vector3.Dot(-hitNormal, velocity.normalized) > 0.5f)
        {
            velocity = hitNormal * 10;

            hitNormal.y = 5;

            gravity = new Vector2(0, 30);
        }
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {

        Vector3 inputDir = controller.GetInput();

        velocity.y -= gravity.y * deltaTime;

        if (velocity.y <= -maxFallSpeed)
        {
            velocity.y = -maxFallSpeed;
        }

        if (controller.manager.Motor.GroundingStatus.IsStableOnGround && velocity.y <= 0)
        {
            velocity.y = 0;
        }

        var comparison = Vector3.Dot(launchDir, inputDir);

        Debug.Log("COMPARISON: " + comparison);
        if (comparison < -0.2) // ! 0.1
        {
            var cancellationVector = ((launchDir * -1).normalized * Time.deltaTime) * mainDirCancellationFactor;

            cancellationVector.y = 0;

            velocity += cancellationVector;
        }

        var rightOfVelocity = Vector3.Cross(Vector3.up, launchDir);

        velocity += Vector3.Project(inputDir, rightOfVelocity) * HorizontalDirAccelerationFactor * Time.deltaTime;


        currentVelocity = velocity;
    }
}
