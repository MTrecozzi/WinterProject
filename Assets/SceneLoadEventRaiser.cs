using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadEventRaiser : MonoBehaviour
{
    public BinaryCrossSceneReference levelLoadMessage;
    public LoadEventChannelSO channel;

    [SerializeField]
    public GameSceneSO[] level2Necessities;

    private void Awake()
    {
        levelLoadMessage.BinaryMessage += HandleLoadRequest;
    }

    private void HandleLoadRequest(bool obj)
    {
        if (obj)
        {
            channel.RaiseEvent(level2Necessities);
        }
    }
}
