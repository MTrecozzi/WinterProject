using KinematicCharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStateManager : MonoBehaviour
{


    private List<MovementStateTransition> _stateTransitions = new List<MovementStateTransition>();

    public MovementState CurrentState => curMovementState;
    // should be made private
    public MovementState curMovementState;

    public DefaultMoveStateBehaviour defaultMoveStateBehaviour;

    public MovementState defaultMoveState;
    
    public Queue<Vector3> velocityQueue = new Queue<Vector3>();
    public event Action<MovementState, MovementState> OnStateChanged;

    public Queue<MovementState> stateQueue = new Queue<MovementState>();
    public KinematicCharacterMotor Motor;

    private void Awake()
    {
        Debug.LogWarning("Because Movement States can't be accessed through the inspector, I'm going through a mono reference to get defaultMoveState state");

        defaultMoveState = defaultMoveStateBehaviour.defaultMoveState;
    }

    public void AddTransition(MovementState from, Func<bool> condition, MovementState to)
    {
        var stateTransition = new MovementStateTransition(from, condition, to);
        _stateTransitions.Add(stateTransition);
    }

    public void AddTransition(MovementStateTransition transition)
    {
        _stateTransitions.Add(transition);
    }



    /// <summary>
    ///  This is currently being called each frame by the default movement state, sloppy implementation, but it gives us precision
    ///  over when in the loop the manager checks to do transition checks.
    /// </summary>
    public void CheckForConditions()
    {
        MovementStateTransition transition = CheckForTransition();

        if (transition != null)
        {
            // then we want to switch to new state
            SetMovementState(transition.ToState);
        }
    }

    private MovementStateTransition CheckForTransition()
    {

        Debug.LogWarning("Tricky Destination Handled code, transition.Condition()" +
            " is calling code that may decide to handle itself, if this is the case," +
            " this method much return null ( don't transition our selves, because the check itself took care of it), which it currently does" +
            "return null right now");

        foreach (var transition in _stateTransitions)
        {
            // if we find a transition who's condition has been met, we'll return it
            // if dictionaries are more performant, it'll be worth looking into
            if (transition.FromState == curMovementState && transition.Condition() && !transition.destinationHandled)
            {
                // if we are in the desired from state of the transition, and the condition of the transition is met, return this transition from our list.
                return transition;
            }
        }

        return null;
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

        if (curMovementState != null)
        {
            OnStateChanged?.Invoke(newState, curMovementState);
        }
        

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
