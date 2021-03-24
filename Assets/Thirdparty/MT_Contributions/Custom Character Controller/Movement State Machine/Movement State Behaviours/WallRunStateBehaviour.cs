using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunStateBehaviour : MoveStateBehaviour
{
    [SerializeField]
    public WallRunState wallRunState;

    public override MovementState[] GetManagedMoveStates()
    {
        return new MovementState[] { wallRunState };
    }

    public void StartState(Vector3 _surfaceParrallel, Vector3 _curWallNormal) => wallRunState.StartState(_surfaceParrallel, _curWallNormal);

}
