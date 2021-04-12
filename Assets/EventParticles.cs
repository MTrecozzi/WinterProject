using KinematicCharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventParticles : MonoBehaviour
{

    public MTCharacterController controller;
    public ParticleSystem landingParticles;

    public ParticleSystem longJumpParticles;

    public MoveStateManager manager;



    // Start is called before the first frame update
    void Awake()
    {
        Debug.LogWarning("this class should not reference MTCharacter controller, all these events should be managed and invoked from MoveStateManager");

        controller.OnPlayerLanded += PlayParticle;
        controller.OnPlayerJump += PlayParticle;

        manager.OnStateChanged += HandleStateChanged;
    }


    // [ ! ] create an particle state def class, and a list of them managed in this class, just like with animations
    private void HandleStateChanged(Type newState, Type oldState)
    {

        if (oldState == typeof(WallRunStateBehaviour) || newState == typeof(WallRunStateBehaviour))
        {
            PlayParticle();
        }

        if (newState == typeof(LongJumpState))
        {

            if (longJumpParticles != null)
            {
                longJumpParticles.Play();
            }
           
        }
    }

    private void PlayParticle()
    {
        landingParticles.Play();
    }

}
