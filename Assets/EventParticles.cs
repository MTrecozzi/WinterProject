using KinematicCharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventParticles : MonoBehaviour
{

    public MTCharacterController controller;
    public ParticleSystem system;

    public MoveStateManager manager;



    // Start is called before the first frame update
    void Awake()
    {
        Debug.LogWarning("this class should not reference MTCharacter controller, all these events should be managed and invoked from MoveStateManager");

        controller.OnPlayerLanded += PlayParticle;
        controller.OnPlayerJump += PlayParticle;

        manager.OnStateChanged += HandleStateChanged;
    }

    private void HandleStateChanged(Type newState, Type oldState)
    {

        if (oldState == typeof(WallRunStateBehaviour) || newState == typeof(WallRunStateBehaviour))
        {
            PlayParticle();
        }
    }

    private void PlayParticle()
    {
        system.Play();
    }

}
