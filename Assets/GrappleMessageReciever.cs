using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   


public class GrappleMessageReciever : MonoBehaviour
{
    public Image crosshair;
    public BinaryCrossSceneReference CheckReference;
    public BinaryCrossSceneReference EnabledReference;


    private void Awake()
    {
        CheckReference.BinaryMessage += ChangeCrossHairColor;
        EnabledReference.BinaryMessage += ChangeCrosshairActivation;
    }

    private void ChangeCrossHairColor(bool value)
    {

        if(value)
        {
            crosshair.color = Color.green;
        }
        else
        {
            crosshair.color = Color.white;
        }

    }

    private void ChangeCrosshairActivation(bool val)
    {
        crosshair.enabled = val;
    }



}
