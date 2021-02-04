using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnCollectibleEventExample : MonoBehaviour
{

    public CollectibleEventChannel channel;

    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        channel.CollectibleEvent += PlayAudioSource;
    }

    private void PlayAudioSource(Transform obj)
    {
        Debug.Log("Play Collectible Collected Audio Source");

        if (source != null)
        {
            source.Play();
        }
    }
}
