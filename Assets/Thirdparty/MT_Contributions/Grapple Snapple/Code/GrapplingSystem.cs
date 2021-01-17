using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingSystem : MonoBehaviour, ICharacterController
{

    // reference the raycast system
    public RaycastSystem rayCastSystem;

    public KinematicCharacterMotor motor;
    private ICharacterController defaultCharacterController;

    Vector3 pointToZipTo;
    public float grappleSpeed = 60f;


    Vector3 movementVector;

    private bool GrappleEnabled = false;
    public bool grappleEnabled
    {
        get { return GrappleEnabled; }
        set { GrappleEnabled = value;  }
    }
    private void Start()
    {
        defaultCharacterController = motor.CharacterController;
    }

    public void ShootGrapple()
    {

        // returns the point we zip too

        pointToZipTo = Vector3.zero;

        // set pointToZipTo = the point from a raycast

        RaycastHit hitFromRayCastSystem = rayCastSystem.CastRayFromScreen(Mathf.Infinity);

        pointToZipTo = hitFromRayCastSystem.point;
        //Temp make it higher
        pointToZipTo += new Vector3(0, 1, 0);
        // Zip to point

        // if we hit did hit something only then,
        if (hitFromRayCastSystem.collider != null)
        {
            StartGrapple();

            Debug.Log("Latched to: " + hitFromRayCastSystem.transform.name);
        }

        Debug.Log("Grapple Started");
    }

    public void LateUpdate()
    {
        if (GrappleEnabled)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ShootGrapple();
            }

            // if we are at the grapple point
            if ((pointToZipTo - motor.transform.position).magnitude <= 0.5 || Input.GetKeyUp(KeyCode.E))
            {
                EndGrapple();
            }
        }
    }

    public void StartGrapple()
    {

        if (motor.GroundingStatus.IsStableOnGround == true)
        {
            return;
        }

        movementVector = (pointToZipTo - motor.transform.position);

        motor.MoveCharacter(motor.transform.position + Vector3.up * 0.3f);

        Debug.Log(movementVector);

        motor.CharacterController = this;
    }


    public void EndGrapple()
    {
        motor.CharacterController = defaultCharacterController;
        
        Debug.Log("Grapple Ended");
    }

    public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {

        var z = movementVector;

        z.y = 0;

        currentRotation = Quaternion.LookRotation(z, Vector3.up);
    }

    public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        currentVelocity = movementVector.normalized * grappleSpeed;
    }

    public void BeforeCharacterUpdate(float deltaTime)
    {
        
    }

    public void PostGroundingUpdate(float deltaTime)
    {
        
    }

    public void AfterCharacterUpdate(float deltaTime)
    {
        
    }

    public bool IsColliderValidForCollisions(Collider coll)
    {
        return true;
    }

    public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
        
    }

    public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
        
    }

    public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
    {
        
    }

    public void OnDiscreteCollisionDetected(Collider hitCollider)
    {
        
    }
}
