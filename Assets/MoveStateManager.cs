using KinematicCharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStateManager : MonoBehaviour
{
    public MovementState defaultMoveState;
    public MovementState curMovementState;
    public Queue<Vector3> velocityQueue = new Queue<Vector3>();
    public event Action<MovementState, MovementState> OnStateChanged;

    public KinematicCharacterMotor Motor;

    // this needs to be seperate responsibility
    // we need to sperate the KinemaCharacter from the Movement State
    public void SetPropulsionForce(Vector3 newMomentum) // Tell the character to tell its current state to handle an incoming override momentum force
    {
        curMovementState.InformStatePropulsionForce(newMomentum);
    }

    public void SetMovementState(MovementState newState)
    {
        // clean up old state
        curMovementState.CleanUp();

        OnStateChanged?.Invoke(newState, curMovementState);

        // currentState = newState
        curMovementState = newState;
        // initialize incoming state
        curMovementState.Initialize();

        // velocityShiftQueue.Pop() if any

        if (velocityQueue.Count > 0)
        {
            Motor.BaseVelocity = velocityQueue.Dequeue();
        }

        // Motor.CharacterController = newState;
        Motor.CharacterController = curMovementState;
    }

    public void SetDefaultMovementState()
    {
        SetMovementState(this.defaultMoveState);
    }
}
