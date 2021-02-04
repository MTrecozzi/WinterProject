using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiantCharacterController : MonoBehaviour, ICharacterController
{

    public LayerMask world;

    public KinematicCharacterMotor Motor;

    // replace with better input system

    private bool JumpBuffer;

    [SerializeField]
    private Vector3 input;
    private Vector3 wallRunDir;

    [SerializeField]
    bool wallRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        Motor.CharacterController = this;
    }

    // Update is called once per frame
    void Update()
    {

        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));


        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpBuffer = true;
        }

        RaycastHit hitInfo;

        Physics.Raycast(transform.position, transform.forward, out hitInfo, 1f, world);

        var vec1 = transform.forward;

        var vec2 = hitInfo.normal;

        vec1.y = 0;

        vec2.y = 0;

        if (Vector3.Dot(vec1, vec2) >= 0)
        {
            wallRunDir = Vector3.zero;
        } else if (!Motor.GroundingStatus.IsStableOnGround)
        {
            wallRunDir = transform.forward;

            wallRunning = true;
        }

        if (input.magnitude < 1)
        {
            wallRunning = false;
        }

    }

    public void AfterCharacterUpdate(float deltaTime)
    {
        
    }

    public void BeforeCharacterUpdate(float deltaTime)
    {
        
    }

    public bool IsColliderValidForCollisions(Collider coll)
    {
        return true;
    }

    public void OnDiscreteCollisionDetected(Collider hitCollider)
    {
      
    }

    public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
 
    }

    public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {

        // for each hit while moving, compile each hit into a final wall run direction and boolean setting, that will be used by UpdateVelocity

    }

    public void PostGroundingUpdate(float deltaTime)
    {
        
    }

    public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
    {
    
    }

    public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {

        if (input.magnitude >= 1)
        {
            currentRotation = Quaternion.LookRotation(input, Motor.CharacterUp);
        }

        
    }

    public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {

        

        // utilize the ref parameters!

        if (JumpBuffer && Motor.GroundingStatus.IsStableOnGround)
        {
            Motor.ForceUnground();

            // use a jump Vector for finer tune controls on slopes and or walls, use stop gaps in characters motion for feedback and responsiveness
            currentVelocity += Vector3.up * 5f;
        }

        JumpBuffer = false;

        if (wallRunning)
        {

            var prevVelo = currentVelocity;

            currentVelocity = wallRunDir * 5 * input.magnitude;

            if (prevVelo.y > currentVelocity.y)
            {
                currentVelocity.y = prevVelo.y;

                currentVelocity += Physics.gravity * Time.deltaTime;

                if (currentVelocity.y <= 0)
                {
                    currentVelocity.y = 0;
                }
            }

            return;

        }

        currentVelocity += Physics.gravity * Time.deltaTime;
        currentVelocity = new Vector3(input.x * 5, currentVelocity.y, input.z * 5);

        // reference other character controller for easy keeping track of things like was jump consumed this frame.
    }

    
}
