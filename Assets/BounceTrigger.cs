using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTrigger : MonoBehaviour
{
    public BinaryCrossSceneReference UxEvent;

    public event Action OnPlayerBounced;

    public float bounceHeight;
    public float bufferedJumpHeight;
    //public float timeToHeight;

    private MTCharacterController controller;
    private DefaultMoveStateBehaviour defaultMoveState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetLaunchSource()
    {
        float magnitudeOfFoce = Mathf.Sqrt(2 * -defaultMoveState.defaultMoveState.Gravity.y * bounceHeight);

        return transform.up.normalized * magnitudeOfFoce;


    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Player"))
        {

            if (controller == null || defaultMoveState == null)
            {
                controller = other.transform.GetComponent<MTCharacterController>();
                defaultMoveState = other.transform.GetComponent<DefaultMoveStateBehaviour>();
            }

            float magnitudeOfFoce = Mathf.Sqrt(2 * -defaultMoveState.defaultMoveState.Gravity.y * bounceHeight);

            // using state dependent setPropulsionForce
            controller.SetPropulsionForce(transform.up.normalized * magnitudeOfFoce);



            if (!(transform.up.normalized == Vector3.up))
            {
                Debug.LogWarning("Crappy Air Dampening Implementation");
                defaultMoveState.DampenAirAccel();
            }

            OnPlayerBounced?.Invoke();

            defaultMoveState.defaultMoveState.OnLanded();

            if (UxEvent != null)
            {
                UxEvent.InvokeMessage(true);
            }

        }
    }

}
