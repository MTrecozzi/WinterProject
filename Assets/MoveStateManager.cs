using KinematicCharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStateManager : MonoBehaviour
{
    public DefaultMoveStateBehaviour defaultMoveStateBehaviour;

    public MovementState defaultMoveState;
    public MovementState curMovementState;
    public Queue<Vector3> velocityQueue = new Queue<Vector3>();
    public event Action<MovementState, MovementState> OnStateChanged;

    public Queue<MovementState> stateQueue = new Queue<MovementState>();
    public KinematicCharacterMotor Motor;

    private void Awake()
    {
        Debug.LogWarning("Because Movement States can't be accessed through the inspector, I'm going through a mono reference to get defaultMoveState state");

        defaultMoveState = defaultMoveStateBehaviour.defaultMoveState;
    }

    private void FixedUpdate()
    {
        if (curMovementState != null)
        {
            for (int i = 0; i < curMovementState.transitions.Count; i++)
            {
                if (curMovementState.transitions[i].Condition() == true)
                {
                    Debug.Log("TRANSITION TRUE: Transition to " + curMovementState.transitions[i].ToString());

                    SetMovementState(curMovementState.transitions[i].ToState);
                }
            }

        } else
        {
            Debug.LogError("Cur MovementState is null");
        }
    }

    // this needs to be seperate responsibility
    // we need to sperate the KinemaCharacter from the Movement State
    public void SetPropulsionForce(Vector3 newMomentum) // Tell the character to tell its current state to handle an incoming override momentum force
    {
        curMovementState.InformStatePropulsionForce(newMomentum);
    }

    public void SetNextState()
    {
        if (stateQueue.Count > 0)
        {
            SetMovementState(stateQueue.Dequeue());
        } else
        {
            SetDefaultMovementState();
        }
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
