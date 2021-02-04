using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SerializedEventChannels/CollectibleEventChannel")]
public class CollectibleEventChannel : ScriptableObject
{
    public CollectibleType type;

    public event Action<Transform> CollectibleEvent;

    public void Invoke(Transform transform)
    {
        CollectibleEvent.Invoke(transform);
    }
}

public enum CollectibleType
{
    YellowOrb,
}


