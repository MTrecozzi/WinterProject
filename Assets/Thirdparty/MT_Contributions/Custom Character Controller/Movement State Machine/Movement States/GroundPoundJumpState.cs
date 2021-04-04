using UnityEngine;

[System.Serializable] 
public class GroundPoundJumpState : MovementState
{
    public float timeToReachTop;
    public float JumpHeight;

    private Vector3 velocity;
    private Vector3 Gravity;

    public override void Initialize()
    {
        base.Initialize();

        controller.manager.Motor.ForceUnground();

        velocity = Vector3.zero;

        this.Gravity = new Vector3(0, -(2 * JumpHeight / Mathf.Pow(timeToReachTop, 2)), 0);

        velocity.y = (2 * JumpHeight / timeToReachTop);
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {

        if (velocity.y <= 0.5f)
        {
            Gravity = new Vector3(0, -10, 0);
        }

        velocity.y += Gravity.y * deltaTime;
        currentVelocity = velocity;
    }

}
