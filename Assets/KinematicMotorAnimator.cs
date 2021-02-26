using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicMotorAnimator : MonoBehaviour
{
    public Animator anim;
    public KinematicCharacterMotor motor;
    public MTCharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        
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
            anim.SetBool("GroundPound", controller.curMovementState.GetType() == typeof(GroundPoundState));
            anim.SetBool("Dashing", controller.curMovementState.GetType() == typeof(DashState));
            anim.SetBool("WallRun", controller.curMovementState.GetType() == typeof(WallRunState));
        }



        


    }

    public void SetAnimBool(string name, bool value)
    {

    }
}


