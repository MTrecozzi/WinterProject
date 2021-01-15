using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;


public enum fistState
{
    hitting, resting, toHitting, toResting
}



public class FisterPrototype : MonoBehaviour, IMoverController
{
    public PhysicsMover Mover;

    public Transform fistTarget;

    public Transform fistRester;

    public fistState fisterState;
    [Range(0, 1)]
    public float speed = 0.5f;


    public float hitTime;
    public float hittingTime;
    public float lerpTime;

    public float TimeTryingToHit = 0.5f;
    public float TimeTryingToRest = 0.5f;
    


    private bool requestPunch = false;

    private float toHittingTime = 0f;
    private float toRestingTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        fisterState = fistState.resting;

        Mover.MoverController = this;
    }

    // Update is called once per frame


    private void LateUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            requestPunch = true;
        }
    }




    public void StartedLerping(fistState fState)
    {
        hitTime = Time.time;
        fisterState = fState;
    }

    public Vector3 ReLerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerptime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;

        float percentageComplete = timeSinceStarted / lerptime;

        return Vector3.Lerp(start, end, percentageComplete);

    }

    public void UpdateMovement(out Vector3 goalPosition, out Quaternion goalRotation, float deltaTime)
    {


        Vector3 tempPos = Vector3.zero;


        switch (fisterState)
        {
            case fistState.resting:
                tempPos = Vector3.Lerp(transform.position, fistRester.position, speed);
                //Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)
                if (requestPunch)
                {
                    requestPunch = false;
                    StartedLerping(fistState.toHitting);

                }
                break;

            case fistState.hitting:

                tempPos = Vector3.Lerp(transform.position, fistTarget.position, 2f);
                hittingTime += Time.deltaTime;
                //(hittingTime >= 0.01f && Input.GetMouseButton(0)) || !Input.GetMouseButton(0) || Input.GetMouseButtonUp(0)
                if (hittingTime > 0.01f)
                {

                    StartedLerping(fistState.toResting);
                }

                break;

            case fistState.toResting:
                toRestingTime += deltaTime;
                tempPos = ReLerp(transform.position, fistRester.position, hitTime, lerpTime);
                if (toRestingTime > TimeTryingToHit || Vector3.Distance(transform.position, fistRester.position) <= 0.3f)
                {

                    fisterState = fistState.resting;
                    toRestingTime = 0f;
                }
                break;

            case fistState.toHitting:
                toHittingTime += deltaTime;
                tempPos = ReLerp(transform.position, fistTarget.position, hitTime, lerpTime);
                if (toHittingTime > TimeTryingToRest || Vector3.Distance(transform.position, fistTarget.position) <= 0.3f)
                {

                    fisterState = fistState.hitting;
                    hittingTime = 0;
                    toHittingTime = 0f;
                }

                break;
        }


        goalPosition = tempPos;
        goalRotation = Quaternion.identity;
    }
}


