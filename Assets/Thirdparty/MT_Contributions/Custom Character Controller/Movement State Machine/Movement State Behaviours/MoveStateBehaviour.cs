using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveStateBehaviour : MonoBehaviour
{
    public abstract MovementState[] GetManagedMoveStates();

    public void SetReferences(MTCharacterController _controller, DefaultMoveStateBehaviour _defaultMoveState, KinematicCharacterMotor _motor)
    {
       foreach (var x in GetManagedMoveStates())
        {
            x.SetReferences(_controller, _defaultMoveState, _motor);
        }
    }

}
