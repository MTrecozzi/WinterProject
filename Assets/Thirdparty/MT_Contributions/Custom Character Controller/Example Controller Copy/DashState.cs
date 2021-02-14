using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : MovementState
{

    public float dashEndMultiplier = 0.5f;

    public float distance;
    public float timeToReach;

    public float mushroomDashEndMult = 1f;

    private float t;

    public MTCharacterController defaultController;
    public BinaryCrossSceneReference abilityEventReference;

    public LayerMask ignoreLayers;

    private Vector3 dir;

    private bool initiallyGrounded = false;

    private Vector3 dashVelocity;

    private bool mushroomDashed;

    public override void InformStatePropulsionForce(Vector3 newMomentum)
    {
        // exit this state into character default
        defaultController.SetDefaultMovementState();

        if (newMomentum.normalized == Vector3.up)
        {
            newMomentum += dashVelocity * mushroomDashEndMult;
            mushroomDashed = true;

            // invoke mushroom dash event
        }

        // call on default state's default Implementation
        base.InformStatePropulsionForce(newMomentum);
    }


    public void StartDash(Vector3 direction)
    {

        t = 0;
        dir = transform.forward;
        defaultController.SetMovementState(this);

        abilityEventReference.InvokeMessage(true);

        initiallyGrounded = defaultController.Motor.GroundingStatus.IsStableOnGround;

        Debug.Log("InitiallyGrounded: " + initiallyGrounded);

        dashVelocity = distance / timeToReach * dir.normalized; // set vel initially

    }

    public void EndDash()
    {

        abilityEventReference.InvokeMessage(false);
        defaultController.SetDefaultMovementState();
    }

    private void Update()
    {
        if (controller.player.controls.Standard.Dash.triggered && controller.dashPool.currentCharges > 0)
        {

            Debug.Log("Dash Started");
            StartDash(transform.forward);
            controller.dashPool.currentCharges--;
        }
    }

    // something like this

    public void SetHorizontalVelocity(Vector3 vel)
    {
        float y = dashVelocity.y;

        dashVelocity = vel.normalized * (distance / timeToReach);

        dashVelocity.y = y;
    }

    // Working as intended, just need to carry this over into the next state!
    public override bool IsColliderValidForCollisions(Collider coll)
    {
        bool valid = base.IsColliderValidForCollisions(coll) && !(ignoreLayers == (ignoreLayers | (1 << coll.gameObject.layer)));

        // if we detect a non valid collision while dashing, we queue it for the character controller to ignore regardless of states.
        if (!valid)
        {
            controller.passingThroughIgnoredColliders.Add(coll);
        }

        return valid;
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        t += deltaTime;

        if (t > timeToReach)
        {

            // should cache pixel perfect desired end position, and snap there if possible after dash completion, although this wouldn't work if there's interference

            EndDash();

            // !!!!!!!!!!!!!!!!
            // if (jumpBuffered && doublejump is available, consume double jump to combine it with preserved dash momentum.

            dashVelocity = currentVelocity * dashEndMultiplier;

            currentVelocity = dashVelocity;
                return;    
        }

        if (!defaultController.Motor.GroundingStatus.IsStableOnGround && initiallyGrounded)
        {

            Debug.Log("TECH");

            initiallyGrounded = false;

            Debug.LogWarning("COYOTE DASH: Replace with data collection logger");

            defaultController.ResetAbilities();

            // shitty implementation, as it technically should just consume the standard jump
            defaultController.jumpPool.currentCharges = defaultController.jumpPool.maxCharges + 1;
        }

        // return dash velocity
        currentVelocity = dashVelocity;
    }


    public override void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
        
        Debug.Log("Hit: " + hitCollider.name + " Dot Product = " + Vector3.Dot(-hitNormal, dashVelocity.normalized));

        Vector3 surfaceParrallel = dashVelocity - hitNormal * Vector3.Dot(dashVelocity, hitNormal);

        float slideMultiplier = 0.75f;

        dashVelocity = surfaceParrallel.normalized * (distance / timeToReach) * slideMultiplier; // sliding against a wall sh

        //!! Test Structure

        /*
         *  If incoming angle (calculated with dot product), is straight on, allow the option to go upwards or diagnally upwards
         *  
         *  else if coming at an angle, move a long a rnage of surface parrallel normals using the algorithm below
         *  
         *  perhaps the dash halting your upward momentum is breaking the flow, perhaps it halts downward momentum, but not upward momentum.
         */


        // make this a bool, aka dash slides against surfaces



        // THIS ALGORITHM DOESN'T Account for Step Height! Will change direction of dash at even tiny short collisions, do a distance check vertically

        // I guess check collision point against center of character, vertically
        /* Something Like...
         * if (Mathf.Abs( transform.position.y - hitPoint.y) <= 0.3f)
        {

        }
         */






    }
}
