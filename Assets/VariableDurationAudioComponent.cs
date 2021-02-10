using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableDurationAudioComponent : MonoBehaviour
{

    public AudioSource startSound;
    public AudioSource loopSound;
    public AudioSource endSound;

    public BinaryCrossSceneReference abilityReference;

    private bool playing;

    private void Start()
    {
        abilityReference.BinaryMessage += HandleAbilitySignal;
    }

    private void HandleAbilitySignal(bool start)
    {
        if (!playing && start && startSound != null)
        {
            startSound.Play();

            if (loopSound != null)
            {
                loopSound.Play();
            }

            
            playing = true;

        } else if (playing && endSound && endSound != null)
        {
            if (loopSound != null)
            {
                loopSound.Stop();
            }
            endSound.Play();

            playing = false;
        }
    }
}
