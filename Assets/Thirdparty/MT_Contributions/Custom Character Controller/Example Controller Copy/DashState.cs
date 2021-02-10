using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : MonoBehaviour, IVelocityState
{

    public float distance;
    public float timeToReach;

    private float t;

    public MTCharacterController defaultController;

    private Vector3 dir;



    public void StartDash(Vector3 direction)
    {

        t = 0;
        dir = transform.forward;
        defaultController.velocityState = this;

        
    }

    public void EndDash()
    {
        defaultController.velocityState = null;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            Debug.Log("Dash Started");
            StartDash(transform.forward);
        }
    }

    public Vector3 GetVelocity(Vector3 currentVelocity, float deltaTime)
    {
        t += deltaTime;

        if (t > timeToReach)
        {

            // should cache pixel perfect desired end position, and snap there if possible after dash completion, although this wouldn't work if there's interference

            EndDash();
            return Vector3.zero;
            
        }

        

        return distance / timeToReach * dir.normalized;
    }
}
