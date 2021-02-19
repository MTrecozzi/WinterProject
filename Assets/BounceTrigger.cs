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
        float magnitudeOfFoce = Mathf.Sqrt(2 * -controller.Gravity.y * bounceHeight);

        return transform.up.normalized * magnitudeOfFoce;


    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Player"))
        {

            if (controller == null)
            {
                controller = other.transform.GetComponent<MTCharacterController>();
            }

            float magnitudeOfFoce = Mathf.Sqrt(2 * -controller.Gravity.y * bounceHeight);

            // using state dependent setPropulsionForce
            controller.SetPropulsionForce(transform.up.normalized * magnitudeOfFoce);



            if (!(transform.up.normalized == Vector3.up))
            {
                Debug.LogWarning("Crappy Air Dampening Implementation");
                controller.DampenAirAccel();
            }

            OnPlayerBounced?.Invoke();

            controller.OnLanded();

            if (UxEvent != null)
            {
                UxEvent.InvokeMessage(true);
            }

        }
    }

}
