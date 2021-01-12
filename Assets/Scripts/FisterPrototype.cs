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



    // Start is called before the first frame update
    void Start()
    {
        fisterState = fistState.resting;

        Mover.MoverController = this;
    }

    // Update is called once per frame
    void Update()
    {
            
            
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
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                {

                    StartedLerping(fistState.toHitting);

                }
                break;

            case fistState.hitting:

                tempPos = Vector3.Lerp(transform.position, fistTarget.position, 2f);
                hittingTime += Time.deltaTime;

                if ((hittingTime >= 0.25f && Input.GetMouseButton(0)) || !Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
                {

                    StartedLerping(fistState.toResting);
                }

                break;

            case fistState.toResting:

                tempPos = ReLerp(transform.position, fistRester.position, hitTime, lerpTime);
                if (Vector3.Distance(transform.position, fistRester.position) <= 0.1)
                {

                    fisterState = fistState.resting;
                }
                break;

            case fistState.toHitting:

                tempPos = ReLerp(transform.position, fistTarget.position, hitTime, lerpTime);
                if (Vector3.Distance(transform.position, fistTarget.position) <= 0.1)
                {

                    fisterState = fistState.hitting;
                    hittingTime = 0;
                }

                break;
        }


        goalPosition = tempPos;
        goalRotation = Quaternion.identity;
    }
}


