using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementState : MonoBehaviour, ICharacterController
{

    public MTCharacterController controller;

    public void AfterCharacterUpdate(float deltaTime) => controller.AfterCharacterUpdate(deltaTime);

    public void BeforeCharacterUpdate(float deltaTime) => controller.BeforeCharacterUpdate(deltaTime);

    public bool IsColliderValidForCollisions(Collider coll) => controller.IsColliderValidForCollisions(coll);

    public void OnDiscreteCollisionDetected(Collider hitCollider) => controller.OnDiscreteCollisionDetected(hitCollider);

    public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        => controller.OnGroundHit(hitCollider, hitNormal, hitPoint, ref hitStabilityReport);

    public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        => controller.OnMovementHit(hitCollider, hitNormal, hitPoint, ref hitStabilityReport);

    public void PostGroundingUpdate(float deltaTime)
        => controller.PostGroundingUpdate(deltaTime);

    public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
        => controller.ProcessHitStabilityReport(hitCollider, hitNormal, hitPoint, atCharacterPosition, atCharacterRotation, ref hitStabilityReport);

    public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
        => controller.UpdateRotation(ref currentRotation, deltaTime);

    public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
        => controller.UpdateVelocity(ref currentVelocity, deltaTime);

}
