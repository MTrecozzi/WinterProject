using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUXEventSender : MonoBehaviour
{

    public MTCharacterController character;
    public BinaryCrossSceneReference OnPlayerLanded;
    public BinaryCrossSceneReference OnPlayerDoubleJump;

    // Start is called before the first frame update
    void Start()
    {
        character.OnPlayerLanded += HandlePlayerLand;
        character.OnPlayerDoubleJump += HandlePlayerDoubleJump;
    }

    private void HandlePlayerDoubleJump()
    {
        OnPlayerDoubleJump.InvokeMessage(true);
    }

    private void HandlePlayerLand()
    {
        OnPlayerLanded.InvokeMessage(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
