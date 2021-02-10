using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementState : MonoBehaviour, ICharacterController
{

    public MTCharacterController controller;


    public virtual void Initialize() { }

    public virtual void CleanUp() { }

    public virtual void AfterCharacterUpdate(float deltaTime) => controller.AfterCharacterUpdate(deltaTime);

    public virtual void BeforeCharacterUpdate(float deltaTime) => controller.BeforeCharacterUpdate(deltaTime);

    public virtual bool IsColliderValidForCollisions(Collider coll) => controller.IsColliderValidForCollisions(coll);

    public virtual void OnDiscreteCollisionDetected(Collider hitCollider) => controller.OnDiscreteCollisionDetected(hitCollider);

    public virtual void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        => controller.OnGroundHit(hitCollider, hitNormal, hitPoint, ref hitStabilityReport);

    public virtual void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        => controller.OnMovementHit(hitCollider, hitNormal, hitPoint, ref hitStabilityReport);

    public virtual void PostGroundingUpdate(float deltaTime)
        => controller.PostGroundingUpdate(deltaTime);

    public virtual void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
        => controller.ProcessHitStabilityReport(hitCollider, hitNormal, hitPoint, atCharacterPosition, atCharacterRotation, ref hitStabilityReport);

    public virtual void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
        => controller.UpdateRotation(ref currentRotation, deltaTime);

    public virtual void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
        => controller.UpdateVelocity(ref currentVelocity, deltaTime);

}
