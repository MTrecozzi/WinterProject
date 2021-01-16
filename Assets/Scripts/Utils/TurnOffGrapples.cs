using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffGrapples : MonoBehaviour
{

    public BinaryCrossSceneReference GrappleImageChannel;
    public BinaryCrossSceneReference GrappleActivatorChannel;


    private void Start()
    {
        GrappleImageChannel.InvokeMessage(false);
        GrappleActivatorChannel.InvokeMessage(false);   
    }
}
