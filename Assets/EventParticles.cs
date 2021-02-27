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

    private void HandleStateChanged(ICharacterController arg1, ICharacterController arg2)
    {
        if (arg2.GetType() == typeof(WallRunState) || arg1.GetType() == typeof(WallRunState))
        {
            PlayParticle();
        }
    }

    private void PlayParticle()
    {
        system.Play();
    }

}
