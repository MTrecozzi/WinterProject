using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GroundPoundState : MovementState
{
    public float groundPoundVelocity = 0f;

    public float groundPoundJumpVelocity = 15f;

    // ux event for ground pound anticipation start, actual start, etc

    // will need to play nice with a queueing / buffer system

    // do I create a class for a timed state buffer?


    public override void CleanUp()
    {
        if (controller.Jump.Buffered)
        {
            Debug.LogWarning("State Change Eats Velocity Input, Need a Cross State Queue System");
            // on state enter, we should check to see if an action is buffered, if so do that.
            // can we queue a func!?!?
            //controller.Motor.BaseVelocity

            controller.Jump.EatInput();

            Debug.LogWarning("Ugh, Force Unground, have this internally managed with...");
            // this would enforce better reusability and form
            Debug.LogWarning("External States should us an API to the main Controller, rather than direct acess");
            Motor.ForceUnground();

            controller.manager.velocityQueue.Enqueue(new Vector3(0, groundPoundJumpVelocity, 0));

            Debug.LogWarning("Cross State 'Grace Periods Needed', so that ground pound jump buffer can last after the state, should be managed by PlayerControllerClass 100%, with API's for Enqueing such cross state transitional buffers");

            // remember the dash canceled ground pound can only happing durring the initial ground pound state
            Debug.LogWarning("Explore the backend for dash canceled ground pound -> modified dash state with less momentum halting at end of it! What code is required? Where is it organized? and managed?");

            // do something cool for cancelling out of main ground pound land impact, with good buffering
            // the best words here are, you complete a ground pound, what happens if you buffer a dash out of ground pound landing lag instead of a jump?
            Debug.LogWarning("also explore a cool tech for full groundpound -> ground into dash cancel instead of jump input");
        }
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {

        {
            // Easing of this velocity, in partnership / communication with the pre pound buffer, would make this feel good
            currentVelocity = new Vector3(0, -groundPoundVelocity, 0);
        }

    }

    public override void AfterCharacterUpdate(float deltaTime)
    {
        if (Motor.GroundingStatus.FoundAnyGround)
        {
            controller.SetDefaultMovementState();
        }
    }
}

[System.Serializable]
public class LagTransition : MovementState
{
    [SerializeField]
    public StateBuffer buffer;

    public override void Initialize()
    {
        buffer.Reset();
        buffer.SetActive();
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        currentVelocity = Vector3.zero;

        // if the buffer expires this frame
        if (buffer.Tick(deltaTime))
        {
            controller.manager.SetNextState();
        }
    }

}

[SerializeField]
public class GroundPoundStateBehaviour : MonoBehaviour
{

    public GroundPoundState groundPoundState;
    public MTCharacterController controller;
    [SerializeField]
    public LagTransition groundPoundLagState;

    //private MovementStateTransition GroundPoundInitialJump;

    private void Awake()
    {
        //GroundPoundInitialJump = new MovementStateTransition(InitialGroundPoundJumpCheck, new DashState());
        //groundPoundLagState.transitions.Add(GroundPoundInitialJump);
    }

    public bool InitialGroundPoundJumpCheck()
    {
        return controller.Jump.Buffered && !groundPoundLagState.Motor.GroundingStatus.IsStableOnGround;
    }

    // this badddd, CENTRALIZE INPUT IDIOT
    private void FixedUpdate()
    {
        if (controller.controls.Standard.GroundPound.triggered)
        {

            Debug.LogWarning("Replace with a LagTransition of duration 0.13, that queues into ground pound unless interrupted by inputs");


            controller.manager.SetMovementState(groundPoundLagState);
            controller.manager.stateQueue.Enqueue(groundPoundState);



            //groundPoundState.SetState();
        }
    }

}

[System.Serializable]
public class StateBuffer
{
    public float duration = 1f;
    [SerializeField]
    private float t;

    public bool active = false;

    public bool Tick(float deltaTIme)
    {

        if (!active) return false;

        t += Time.deltaTime;

        bool evnt = t >= duration;

        if (t >= duration)
        {
            t = 0;
            active = false;
        }

        return evnt;
    }

    public void SetActive()
    {
        active = true;
    }

    public void Reset()
    {
        t = 0;
    }


}
