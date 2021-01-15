using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockHookShotScript : MonoBehaviour
{

    public BinaryCrossSceneReference GrappleImageChannel;
    public BinaryCrossSceneReference GrappleActivatorChannel;

    public bool GotCube = false;

    private void OnTriggerEnter(Collider other)
    {
        if(GotCube)
        {
            GrappleImageChannel?.InvokeMessage(true);
            GrappleActivatorChannel?.InvokeMessage(true);

            gameObject.SetActive(false);
        }

    }


    public void SetGotCube(bool value)
    {
        GotCube = value;
    }
}
