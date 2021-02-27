using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementState : MonoBehaviour, ICharacterController
{

    public MTCharacterController controller; // for input + state machine API
    protected DefaultMoveState defaultMoveState; // default move state
    public KinematicCharacterMotor Motor;


    #region Encapsulated Disturbance Handlers, to be called by The Character Controller, which really should be seperated from the default Movment State
    // currently the default movementstate is coupled with the movementstatemanager!
    // movement state manager should handle things such as SetManagerPropulsionForce => currentState.SetPropulsionForce
    public virtual void InformStatePropulsionForce(Vector3 newMomentum) => defaultMoveState.InformStatePropulsionForce(newMomentum); // the default implementation for state propulsion
                                                                                                                               // the default is the current character way.

    #endregion

    //public virtual float GetAirAcceleration() => controller.AirAccelerationSpeed;

    public virtual void Initialize() { }

    public virtual void CleanUp() { }

    public virtual void AfterCharacterUpdate(float deltaTime) => defaultMoveState.AfterCharacterUpdate(deltaTime);

    public virtual void BeforeCharacterUpdate(float deltaTime) => defaultMoveState.BeforeCharacterUpdate(deltaTime);

    public virtual bool IsColliderValidForCollisions(Collider coll) => defaultMoveState.IsColliderValidForCollisions(coll);

    public virtual void OnDiscreteCollisionDetected(Collider hitCollider) => defaultMoveState.OnDiscreteCollisionDetected(hitCollider);

    public virtual void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        => defaultMoveState.OnGroundHit(hitCollider, hitNormal, hitPoint, ref hitStabilityReport);

    public virtual void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        => defaultMoveState.OnMovementHit(hitCollider, hitNormal, hitPoint, ref hitStabilityReport);

    public virtual void PostGroundingUpdate(float deltaTime)
        => defaultMoveState.PostGroundingUpdate(deltaTime);

    public virtual void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
        => defaultMoveState.ProcessHitStabilityReport(hitCollider, hitNormal, hitPoint, atCharacterPosition, atCharacterRotation, ref hitStabilityReport);

    public virtual void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
        => defaultMoveState.UpdateRotation(ref currentRotation, deltaTime);

    public virtual void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
        => defaultMoveState.UpdateVelocity(ref currentVelocity, deltaTime);

}
