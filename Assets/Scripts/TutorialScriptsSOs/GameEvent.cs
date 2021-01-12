using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFloatVar", menuName = "CustomSO/Types/FloatVaraible")]
public class FloatVariable : ScriptableObject
{
    public float value;
}


[CreateAssetMenu(fileName = "NewGameEvent", menuName = "CustomSO/Events/GameEvent")]
public class GameEvent : ScriptableObject
{

    private List<GameEventListener> listeners = new List<GameEventListener>();
    public void RegisterListerner(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }

    public void Raise()
    {
        for(int i = listeners.Count -1; i >= 0; --i)
        {
            listeners[i].RaiseEvent();
        }
    }



}
