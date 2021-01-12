using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BinaryCrossSceneReciever : MonoBehaviour
{

    public BinaryCrossSceneReference reference;

    public UnityEvent<bool> OnMessageRecieve;

    // Start is called before the first frame update
    void Awake()
    {
        reference.BinaryMessage += HandleRecieveMessage;
    }

    private void HandleRecieveMessage(bool value)
    {
        OnMessageRecieve.Invoke(value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
