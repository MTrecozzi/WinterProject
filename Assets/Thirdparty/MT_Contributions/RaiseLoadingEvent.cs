using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RaiseLoadingEvent : MonoBehaviour
{
    public LoadEventChannelSO loadingChannel;
    public GameSceneSO[] scenesToLoad;

    public void RaiseLoadEvent()
    {
        loadingChannel.RaiseEvent(scenesToLoad);
    }
}
