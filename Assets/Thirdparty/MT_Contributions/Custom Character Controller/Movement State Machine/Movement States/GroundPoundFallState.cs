using UnityEngine;
// could literally be replaced with Constant Velocity State
[System.Serializable]
public class GroundPoundFallState : MovementState
{
    public float groundPoundVelocity = 0f;

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {

        {
            // Easing of this velocity, in partnership / communication with the pre pound buffer, would make this feel good
            currentVelocity = new Vector3(0, -groundPoundVelocity, 0);
        }

    }
}
