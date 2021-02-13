using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceTrigger : MonoBehaviour
{


    public BinaryCrossSceneReference UxEvent;

    public float bounceHeight;
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

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Player"))
        {

            if (controller == null)
            {
                controller = other.transform.GetComponent<MTCharacterController>();
            }

            controller.Motor.ForceUnground();

            float magnitudeOfFoce = Mathf.Sqrt(2 * -controller.Gravity.y * bounceHeight);

            controller.Motor.BaseVelocity = transform.up.normalized * magnitudeOfFoce;


            controller.SetPropulsionForce();

            controller.OnLanded();

            if (UxEvent != null)
            {
                UxEvent.InvokeMessage(true);
            }

        }
    }

}
