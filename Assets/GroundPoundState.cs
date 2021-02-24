using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPoundState : MovementState
{

    public float groundPoundVelocity = 0f;

    public float groundPoundJumpVelocity = 15f;

    public StateBuffer prePound;

    // ux event for ground pound anticipation start, actual start, etc

    // will need to play nice with a queueing / buffer system
    
    // do I create a class for a timed state buffer?

    public void SetState()
    {

        if (controller.Motor.GroundingStatus.FoundAnyGround)
        {
            return;
        } else
        {
            prePound.SetActive();
            controller.SetMovementState(this);
        }

        
    }

    public override void CleanUp()
    {
        if (controller.player.Jump.Buffered)
        {
            Debug.LogWarning("State Change Eats Velocity Input, Need a Cross State Queue System");
            // on state enter, we should check to see if an action is buffered, if so do that.
            // can we queue a func!?!?
            //controller.Motor.BaseVelocity

            controller.player.Jump.EatInput();

            Debug.LogWarning("Ugh, Force Unground, have this internally managed with...");
            // this would enforce better reusability and form
            Debug.LogWarning("External States should us an API to the main Controller, rather than direct acess");
            controller.Motor.ForceUnground();
            controller.velocityQueue.Enqueue(new Vector3(0, groundPoundJumpVelocity, 0));

            Debug.LogWarning("Cross State 'Grace Periods Needed', so that ground pound jump buffer can last after the state, should be managed by PlayerControllerClass 100%, with API's for Enqueing such cross state transitional buffers");
        }
    }

    // this badddd, CENTRALIZE INPUT IDIOT
    private void FixedUpdate()
    {
        if (controller.player.controls.Standard.GroundPound.triggered)
        {
            SetState();
        }
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {

        // use this for UX and buffers
        bool poundPoundStart = prePound.Tick(deltaTime);


        

        // needs smooth anticipation + acceleration towards final fast fall speed

        if (prePound.active)
        {
            currentVelocity = new Vector3(0, 0, 0);
        }
        else
        {
            // Easing of this velocity, in partnership / communication with the pre pound buffer, would make this feel good
            currentVelocity = new Vector3(0, -groundPoundVelocity, 0);
        }
   
    }

    public override void AfterCharacterUpdate(float deltaTime)
    {
        if (controller.Motor.GroundingStatus.FoundAnyGround)
        {
            controller.SetDefaultMovementState();
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
