using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessageOnPlayerEnter : MonoBehaviour
{
    public BinaryCrossSceneReference reference;

    public string tagToSearch = "Player";

    public bool DataToSend = true;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(tagToSearch))
        {
            reference.InvokeMessage(DataToSend);
        }
    }

}
