using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoEventTrigger : MonoBehaviour
{

    public MoveStateManager manager;
    public SnapShotCamera cam;

    public bool picTaken = false;

    public void TakePhoto()
    {
        cam.CallTakeSnapShot();

        picTaken = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (manager == null)
            {
                manager = other.GetComponent<MoveStateManager>();

                
            }

            Subscribe();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           UnSubscribe();
        }
    }

    public void Subscribe()
    {
        manager.OnStateChanged += HandleStateChanged;
    }

    public void UnSubscribe()
    {
        manager.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(Type newState, Type oldState)
    {
        if ( !picTaken && newState == typeof(WallRunState))
        {
            TakePhoto();
        }
    }
}
