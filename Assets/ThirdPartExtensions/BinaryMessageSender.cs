using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryMessageSender : MonoBehaviour
{

    public BinaryCrossSceneReference crossSceneReference;

    public void SendMessage(bool value)
    {
        crossSceneReference.InvokeMessage(value);
    }






}
