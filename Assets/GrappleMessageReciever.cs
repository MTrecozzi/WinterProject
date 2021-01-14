using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   


public class GrappleMessageReciever : MonoBehaviour
{
    public Image crosshair;
    public BinaryCrossSceneReference reference;

    private void Awake()
    {
        reference.BinaryMessage += ChangeCrossHairColor;
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



}
