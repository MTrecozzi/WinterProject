using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleTargetComponent : MonoBehaviour
{
    public BinaryCrossSceneReference GrappleCheckChannel;

    private void OnMouseEnter()
    {
        GrappleCheckChannel.InvokeMessage(true);
    }

    private void OnMouseExit()
    {
        GrappleCheckChannel.InvokeMessage(false);
    }
}
