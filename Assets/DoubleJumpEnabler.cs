using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpEnabler : MonoBehaviour
{
    public BinaryCrossSceneReference doubleJumpActivation;
    public MTCharacterController controller;

    // Start is called before the first frame update
    void Awake()
    {
        doubleJumpActivation.BinaryMessage += HandleActivation;
    }

    private void HandleActivation(bool activated)
    {

        controller.doubleJumpEnabled = activated;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
