
using UnityEngine;

public class BufferedAction
{
    public bool Buffered { get; private set; }

    public float bufferDuration = 0.2f;
    private float pressedTime;

    public void Tick()
    {
        // Stupid! pressed time is an moment in time, don't increment it or else we'll never be able to compare current time vs pressed time + buffer
        // pressedTime += Time.deltaTime;

        if (Time.time >= pressedTime + bufferDuration && Buffered)
        {
            Buffered = false;
        }
    }

    public void CallInput()
    {
        Buffered = true;
        pressedTime = Time.time;

        Debug.LogError("INPUT Called");

    }

    public void EatInput()
    {
       

        Buffered = false;
    }


}