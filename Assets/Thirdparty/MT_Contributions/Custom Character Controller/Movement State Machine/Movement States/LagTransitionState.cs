using UnityEngine;

[System.Serializable]
public class LagTransitionState : MovementState
{
    [SerializeField]
    public StateBuffer buffer;

    public override void Initialize()
    {
        buffer.SetStartTime();
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        currentVelocity = Vector3.zero;
    }

}
