using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : MovementState
{

    public float dashEndMultiplier = 0.5f;

    public float distance;
    public float timeToReach;

    private float t;

    public MTCharacterController defaultController;
    public BinaryCrossSceneReference abilityEventReference;

    private Vector3 dir;

    private bool initiallyGrounded = false;



    public void StartDash(Vector3 direction)
    {

        t = 0;
        dir = transform.forward;
        defaultController.OverrideMovementState(this);

        abilityEventReference.InvokeMessage(true);

        initiallyGrounded = defaultController.Motor.GroundingStatus.IsStableOnGround;

        Debug.Log("InitiallyGrounded: " + initiallyGrounded);

    }

    public void EndDash()
    {

        abilityEventReference.InvokeMessage(false);
        defaultController.SetDefaultMovementState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && controller.dashPool.currentCharges > 0)
        {

            Debug.Log("Dash Started");
            StartDash(transform.forward);
            controller.dashPool.currentCharges--;
        }
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

                currentVelocity = currentVelocity * dashEndMultiplier;
                return;    
        }

        if (!defaultController.Motor.GroundingStatus.IsStableOnGround && initiallyGrounded)
        {

            Debug.Log("TECH");

            initiallyGrounded = false;
            defaultController.ResetAbilities();

            // shitty implementation, as it technically should just consume the standard jump
            defaultController.jumpPool.currentCharges = defaultController.jumpPool.maxCharges + 1;
        }

        // return dash velocity
        currentVelocity = distance / timeToReach * dir.normalized;
    }
}
