using UnityEngine;

[System.Serializable]
public class ConstantVelocityState : MovementState
{
    public Vector2 velocityMagnitude;


    public override void Initialize()
    {
        base.Initialize();

        if (velocityMagnitude.y > 0)
        {
            controller.manager.Motor.ForceUnground();
        }
    }

    public override void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        return;
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        
        currentVelocity = controller.transform.forward.normalized *velocityMagnitude.x
            + Vector3.up * velocityMagnitude.y;
    }
}
