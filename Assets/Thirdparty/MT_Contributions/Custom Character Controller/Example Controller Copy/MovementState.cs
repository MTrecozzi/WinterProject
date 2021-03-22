using KinematicCharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementStateTransition
{
    public MovementStateTransition(MovementState fromState, Func<bool> condition, MovementState toState)
    {
        Condition = condition;
        ToState = toState;
        FromState = fromState;
    }

    public Func<bool> Condition;
    public MovementState FromState;
    public MovementState ToState;

    public bool destinationHandled = false;
    
}

[System.Serializable]
public abstract class MovementState : ICharacterController
{

    public MTCharacterController controller; // for input + state machine API
    public DefaultMoveStateBehaviour defaultMoveState; // default move state
    public KinematicCharacterMotor Motor;

    public void SetReferences(MTCharacterController _controller, DefaultMoveStateBehaviour _defaultMoveState, KinematicCharacterMotor _motor)
    {
        controller = _controller;
        defaultMoveState = _defaultMoveState;
        Motor = _motor;
    }


    public MovementState()
    {

    }

    public MovementState(MTCharacterController controller, DefaultMoveStateBehaviour defaultMoveState, KinematicCharacterMotor Motor)
    {
        this.controller = controller;
        this.defaultMoveState = defaultMoveState;
        this.Motor = Motor;
    }



    #region Encapsulated Disturbance Handlers, to be called by The Character Controller, which really should be seperated from the default Movment State
    // currently the default movementstate is coupled with the movementstatemanager!
    // movement state manager should handle things such as SetManagerPropulsionForce => currentState.SetPropulsionForce
    public virtual void InformStatePropulsionForce(Vector3 newMomentum) => defaultMoveState.defaultMoveState.InformStatePropulsionForce(newMomentum); // the default implementation for state propulsion
                                                                                                                               // the default is the current character way.

    #endregion

    //public virtual float GetAirAcceleration() => controller.AirAccelerationSpeed;

    public virtual void Initialize() { }

    public virtual void CleanUp() { }

    public virtual void AfterCharacterUpdate(float deltaTime) => defaultMoveState.defaultMoveState.AfterCharacterUpdate(deltaTime);

    public virtual void BeforeCharacterUpdate(float deltaTime) => defaultMoveState.defaultMoveState.BeforeCharacterUpdate(deltaTime);

    public virtual bool IsColliderValidForCollisions(Collider coll) => defaultMoveState.defaultMoveState.IsColliderValidForCollisions(coll);

    public virtual void OnDiscreteCollisionDetected(Collider hitCollider) => defaultMoveState.defaultMoveState.OnDiscreteCollisionDetected(hitCollider);

    public virtual void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        => defaultMoveState.defaultMoveState.OnGroundHit(hitCollider, hitNormal, hitPoint, ref hitStabilityReport);

    public virtual void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
        => defaultMoveState.defaultMoveState.OnMovementHit(hitCollider, hitNormal, hitPoint, ref hitStabilityReport);

    public virtual void PostGroundingUpdate(float deltaTime)
        => defaultMoveState.defaultMoveState.PostGroundingUpdate(deltaTime);

    public virtual void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
        => defaultMoveState.defaultMoveState.ProcessHitStabilityReport(hitCollider, hitNormal, hitPoint, atCharacterPosition, atCharacterRotation, ref hitStabilityReport);

    public virtual void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
        => defaultMoveState.defaultMoveState.UpdateRotation(ref currentRotation, deltaTime);

    public virtual void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
        => defaultMoveState.defaultMoveState.UpdateVelocity(ref currentVelocity, deltaTime);

}
