using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableAsset/Event")]
public class FistEventSO : ScriptableObject
{
    public event Action<FistActivation> loadFistRequest;


    public void RaisedFistEvent(FistActivation fistEvent)
    {

        if(loadFistRequest != null)
        {
            loadFistRequest.Invoke(fistEvent);
        }
        else
        {
            Debug.LogWarning("no fist events?");
        }

    }
}

