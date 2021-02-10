using UnityEngine;

public interface IVelocityState
{
    public Vector3 GetVelocity(Vector3 currentVelocity, float deltaTime);
}