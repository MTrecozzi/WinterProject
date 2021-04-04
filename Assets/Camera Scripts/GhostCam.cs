using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCam : MonoBehaviour
{
    public float ghostPositionY;
    public Transform CharacterMesh;
    public Camera cam;

    public Transform ghostTransform;

    public Vector3 refVel;

    public KinematicCharacterMotor motor;

    public float desiredSmoothTime;
    public float followSpeed;


    private void Start()
    {
        ghostTransform = this.transform;
    }


    // only invoke when character leaving the ground via jump or fall
    void OnLeaveGround()
    {
        // update Y for behavior 3
        ghostPositionY = CharacterMesh.position.y;
    }

    void LateUpdate()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(CharacterMesh.position + motor.BaseVelocity * Time.deltaTime);

        // behavior 2
        if (viewPos.y > 0.85f || viewPos.y < 0.3f)
        {
            ghostPositionY = CharacterMesh.position.y;
        }
        // behavior 4
        else if (motor.GroundingStatus.IsStableOnGround)
        {
            ghostPositionY = CharacterMesh.position.y;
        }
        // behavior 5
        var desiredPosition = new Vector3(CharacterMesh.position.x, ghostPositionY, CharacterMesh.position.z);

        var vec = Vector3.SmoothDamp(ghostTransform.position, desiredPosition, ref refVel, desiredSmoothTime);

        Vector3 finalPosition = new Vector3(CharacterMesh.position.x, vec.y, CharacterMesh.position.z);

        ghostTransform.position = finalPosition;
    }
}
