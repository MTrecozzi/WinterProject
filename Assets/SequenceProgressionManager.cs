using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using System;

public class SequenceProgressionManager : MonoBehaviour
{
    public Transform progressionOrb;
    public Transform secondPosition;

    public CinemachineVirtualCamera progressCam;

    public DemoSequence[] sequences;

    int i = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void NextSequence()
    {
        if (i < sequences.Length)
        {
            //progressCam.Priority = 11;

            var mySequence = sequences[i].GetSequence();

            mySequence.AppendCallback(new TweenCallback(PlayerCam));

            mySequence.SetAutoKill(false);


            mySequence.Play();

            i++;
        }
    }

    private void PlayerCam()
    {
        progressCam.Priority = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
