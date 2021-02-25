using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicMotorAnimator : MonoBehaviour
{
    public Animator anim;
    public KinematicCharacterMotor motor;

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
    }
}
