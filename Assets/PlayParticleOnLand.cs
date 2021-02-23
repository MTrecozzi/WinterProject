using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleOnLand : MonoBehaviour
{

    public MTCharacterController controller;
    public ParticleSystem system;

    // Start is called before the first frame update
    void Awake()
    {
        controller.OnPlayerLanded += PlayParticle;
        controller.OnPlayerJump += PlayParticle;
    }

    private void PlayParticle()
    {
        system.Play();
    }

}
