using UnityEngine;

[System.Serializable] 
public class GroundPoundJumpState : MovementState
{
    public float timeToReachTop;
    public float JumpHeight;

    public float descentGravity;

    private Vector3 velocity;
    private Vector3 Gravity;

    public float MaxAirControl;

    public float endT;

    public float t;

    private float controlPercentage;

    public bool ElapsedTime()
    {
        return t >= endT;
    }

    public override void Initialize()
    {
        base.Initialize();

        t = 0;

        controller.manager.Motor.ForceUnground();

        velocity = Vector3.zero;

        this.Gravity = new Vector3(0, -(2 * JumpHeight / Mathf.Pow(timeToReachTop, 2)), 0);

        velocity.y = (2 * JumpHeight / timeToReachTop);
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {

        t += deltaTime;

        if (velocity.y <= 0.5f)
        {
            Gravity = new Vector3(0, -descentGravity, 0);
        }

        velocity.x = controller.GetInput().x * Mathf.Lerp(0, MaxAirControl, t / timeToReachTop);
        velocity.z = controller.GetInput().z * Mathf.Lerp(0, MaxAirControl, t / timeToReachTop);

        velocity.y += Gravity.y * deltaTime;
        currentVelocity = velocity;
    }

}
