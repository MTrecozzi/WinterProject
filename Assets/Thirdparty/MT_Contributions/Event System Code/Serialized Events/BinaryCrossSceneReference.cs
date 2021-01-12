using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SerializedEventChannels/BinaryCrossSceneReference", order = 1)]
public class BinaryCrossSceneReference : ScriptableObject
{
    public event Action<bool> BinaryMessage;
    
    public void InvokeMessage(bool value)
    {
        BinaryMessage?.Invoke(value);
    }
}
