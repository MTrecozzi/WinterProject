using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventListener : MonoBehaviour
{
    // the game event instance to register to.
    public GameEvent GameEvent;
    //the unity event responce created for teh event.
    public event Action Response;

    private void OnEnable()
    {
        GameEvent.RegisterListerner(this);
    }

    private void OnDisable()
    {
        GameEvent.UnregisterListener(this); 
    }

    public void RaiseEvent()
    {
        Response.Invoke();
    }
}