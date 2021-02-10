using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBinaryAudioPlayer : MonoBehaviour
{

    public AudioSource source;
    public BinaryCrossSceneReference audioSource;


    public bool playOnStart = true;
    public bool playOnEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.BinaryMessage += HandleSourceEvent;   
    }

    private void HandleSourceEvent(bool start)
    {
        if (start && playOnStart)
        {
            source.Play();
        }

        else if (!start && playOnEnd)
        {
            source.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
