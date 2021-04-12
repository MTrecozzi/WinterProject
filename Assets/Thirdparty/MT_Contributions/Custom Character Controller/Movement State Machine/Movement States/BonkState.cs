using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BonkState : MovementState
{

    public Vector2 bonkVector;

    public Vector3 velocity;
    public float bonkGravity = 55;

    private Quaternion startRot;


    public void SetUp(Vector3 hitNormal)
    {
        velocity = hitNormal.normalized * bonkVector.x;
        velocity.y = bonkVector.y;
        startRot = controller.manager.Motor.TransientRotation;
    }

    public override void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        currentRotation = startRot;
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        velocity.y -= bonkGravity * Time.deltaTime;
        currentVelocity = velocity;
    }
}
