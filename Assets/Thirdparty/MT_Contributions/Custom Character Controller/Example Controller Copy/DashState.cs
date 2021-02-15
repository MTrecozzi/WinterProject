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

    public WallRunState wallRunState;

    public MTCharacterController defaultController;
    public BinaryCrossSceneReference abilityEventReference;

    public LayerMask ignoreLayers;

    private Vector3 dir;

    private Vector3 currentWallNormal;

    private bool initiallyGrounded = false;

    private Vector3 dashVelocity;

    private bool mushroomDashed;

    private bool wallReoriented;

    private bool endState;

    private Vector3 surfaceParrallel;


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

    public override void Initialize()
    {
        t = 0;
        dashVelocity = Vector3.zero;
        mushroomDashed = false;
        wallReoriented = false;
        dashVelocity = Vector3.zero;
        initiallyGrounded = false;
        currentWallNormal = Vector3.zero;
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

    // input handled in fixed update
    // since we're updating input in fixed update, when this code ran in update, it consumed multiple dashes per frame
    private void FixedUpdate()
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
    
    
    // Perhaps we need an inform state wall collision, so that states can do an on wall enter type deal
    // public override void InformStateWallCollision() { }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        t += deltaTime;

        // if we're wall jumping off of a wall
        if (wallReoriented && currentWallNormal != Vector3.zero && controller.player.Jump.Buffered)
        {

            controller.player.Jump.EatInput();

            Debug.Log("WALL JUMPED");

            EndDash();

            // set the controller into a crappy air accel dampened state for a few seconds
            controller.DampenAirAccel();

            // don't reset abilities after wall jump
            //controller.ResetAbilities();

            // dash velocity = current movementum, halted less then usual
            dashVelocity *= (0.7f);

            // add to dash velocity a force from the normal of the wall
            dashVelocity += currentWallNormal.normalized * 10;

            // add a jump to the wall jump
            dashVelocity.y += controller.JumpUpSpeed;

            Debug.Log("Current Wall Normal: " + currentWallNormal);

            // currentVelocity = dashVelocity + wall Jump
            controller.Motor.BaseVelocity = dashVelocity;

            return;
        }

        if (t > timeToReach)
        {

            Debug.Log("WALL ORIENTED: " + wallReoriented);

            if (wallReoriented) // && still on wall
            {
                wallRunState.StartState(surfaceParrallel, currentWallNormal);

                // idiot! Messy returns, this state was being overriden because we didn't return and code kept running

                return;

            }

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

        surfaceParrallel = dashVelocity - hitNormal * Vector3.Dot(dashVelocity, hitNormal);

        float slideMultiplier = 0.75f;

        dashVelocity = surfaceParrallel.normalized * (distance / timeToReach) * slideMultiplier; // sliding against a wall sh

        Debug.LogWarning("Not checking properly for if we're on a wall: Also Need new Logging System");

        if (hitNormal.y == 0)
        {
            wallReoriented = true;
            // Idiot! NOT currentWallNormal = surfaceParrallel.normalized;
            currentWallNormal = hitNormal.normalized;


        }


        


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
