using KinematicCharacterController;
using UnityEngine;

[System.Serializable]
public class DashState : MovementState
{
    [SerializeField]
    public DashState dashState;

    public float dashEndMultiplier = 0.5f;

    public float distance;
    public float timeToReach;

    public float mushroomDashEndMult = 1f;

    public float t;

    public WallRunStateBehaviour wallRunState;

    public BinaryCrossSceneReference abilityEventReference;

    public LayerMask ignoreLayers;

    public Vector3 dir;

    public Vector3 currentWallNormal;

    public bool initiallyGrounded = false;

    public Vector3 dashVelocity;

    private bool mushroomDashed;

    public bool wallReoriented;

    private bool endState;

    public Vector3 surfaceParrallel;

    public float initialDotProduct;

    public float dotProductBonkCutOff = 0.7f;


    public override void InformStatePropulsionForce(Vector3 newMomentum)
    {
        // exit this state into character default
        controller.SetDefaultMovementState();

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

    public override void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        if (wallReoriented == false)
        {

            base.UpdateRotation(ref currentRotation, deltaTime);
        } else
        {

            base.UpdateRotation(ref currentRotation, deltaTime);
            //currentRotation = Quaternion.LookRotation(surfaceParrallel);
        }

        
    }


    public void StartDash(Vector3 direction)
    {

        t = 0;
        dir = controller.transform.forward;
        controller.SetMovementState(this);

        abilityEventReference.InvokeMessage(true);

        initiallyGrounded = Motor.GroundingStatus.IsStableOnGround;

        Debug.Log("InitiallyGrounded: " + initiallyGrounded);

        dashVelocity = distance / timeToReach * dir.normalized; // set vel initially

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
            defaultMoveState.defaultMoveState.passingThroughIgnoredColliders.Add(coll);
        }

        return valid;
    }


    // Perhaps we need an inform state wall collision, so that states can do an on wall enter type deal
    // public override void InformStateWallCollision() { }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        t += deltaTime;

        if (!Motor.GroundingStatus.IsStableOnGround && initiallyGrounded)
        {

            Debug.Log("TECH");

            initiallyGrounded = false;

            Debug.LogWarning("COYOTE DASH: Replace with data collection logger");

            defaultMoveState.ResetAbilities();

            // shitty implementation, as it technically should just consume the standard jump

            defaultMoveState.IncrementTempJumps();

            //defaultMoveState.jumpPool.currentCharges = defaultMoveState.jumpPool.maxCharges + 1;

        }

        // return dash velocity
        currentVelocity = dashVelocity;
    }


    public override void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {

        Debug.Log("Hit: " + hitCollider.name + " Dot Product = " + Vector3.Dot(-hitNormal, dashVelocity.normalized));

        surfaceParrallel = dashVelocity - hitNormal * Vector3.Dot(dashVelocity, hitNormal);

        initialDotProduct = Vector3.Dot(-hitNormal, dashVelocity.normalized);

        Debug.Log("INITIAL DOT PRODUCT: " + initialDotProduct);

        Debug.LogWarning("TEMP, SAME IMPLEMENTATION FOR HEAD ON AND SIDE WALL DASHES, not starting custom head on collision yet");
        // although the branching functionalit later on is changed by the fact that initialDotProduct is variable, it's checked on state end

        if (initialDotProduct < dotProductBonkCutOff || initialDotProduct >= dotProductBonkCutOff)
        {
            float slideMultiplier = 0.75f;

            dashVelocity = surfaceParrallel.normalized * (distance / timeToReach) * slideMultiplier; // sliding against a wall sh

            Debug.LogWarning("Not checking properly for if we're on a wall: Also Need new Logging System");

            if (hitNormal.y == 0)
            {
                wallReoriented = true;
                // Idiot! NOT currentWallNormal = surfaceParrallel.normalized;
                currentWallNormal = hitNormal.normalized;
            }
        }
        else
        {

        }
  
    }
}
