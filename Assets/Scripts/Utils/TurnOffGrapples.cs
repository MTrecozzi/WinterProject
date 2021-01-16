using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffGrapples : MonoBehaviour
{

    public BinaryCrossSceneReference GrappleImageChannel;
    public BinaryCrossSceneReference GrappleActivatorChannel;

    public bool GrappleBool = false;


    private void Start()
    {
        GrappleImageChannel.InvokeMessage(GrappleBool);
        GrappleActivatorChannel.InvokeMessage(GrappleBool);   
    }
}
