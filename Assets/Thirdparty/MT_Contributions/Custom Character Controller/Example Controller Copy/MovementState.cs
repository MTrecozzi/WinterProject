using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementState : MonoBehaviour, ICharacterController
{


    public MTCharacterController controller;


    #region Encapsulated Disturbance Handlers, to be called by The Character Controller, which really should be seperated from the default Movment State
    // currently the default movementstate is coupled with the movementstatemanager!
    // movement state manager should handle things such as SetManagerPropulsionForce => currentState.SetPropulsionForce
    public virtual void InformStatePropulsionForce(Vector3 newMomentum) => controller.InformStatePropulsionForce(newMomentum); // the default implementation for state propulsion
                                                                                                 // the default is the current character way.

    #endregion



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
