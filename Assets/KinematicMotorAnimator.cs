using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicMotorAnimator : MonoBehaviour
{
    public Animator anim;
    public KinematicCharacterMotor motor;
    public MTCharacterController controller;

    public Dictionary<MovementState, string> animStateDefs;

    // Start is called before the first frame update
    void Awake()
    {
        animStateDefs = new Dictionary<MovementState, string>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var momentum = motor.BaseVelocity;

        momentum.y = 0;

        anim.SetFloat("ForwardMomentum", momentum.magnitude);
        anim.SetBool("Grounded", motor.GroundingStatus.FoundAnyGround);




        if (controller.curMovementState == null)
        {
            anim.SetBool("GroundPound", false);
            return;
        } else
        {
            anim.SetBool("GroundPound", controller.curMovementState.GetType() == typeof(GroundPoundFallState));
            anim.SetBool("Dashing", controller.curMovementState.GetType() == typeof(DashState));
            anim.SetBool("WallRun", controller.curMovementState.GetType() == typeof(WallRunState));
        }


        anim.SetBool("CrouchHeld", controller.controls.Standard.GroundPound.ReadValue<float>() >= 0.5f);


        // real guy code, runs second so as to not get overriden by hard coded stuff above

        foreach (KeyValuePair<MovementState, string> e in animStateDefs)
        {
            if (controller.curMovementState == e.Key)
            {
                anim.SetBool(e.Value, true);
            }
        }





    }

    public void SetAnimBool(string name, bool value)
    {

    }
}


