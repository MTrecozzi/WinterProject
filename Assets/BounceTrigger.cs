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
            controller.Motor.BaseVelocity = new Vector3(0, Mathf.Sqrt(2 * -controller.Gravity.y * bounceHeight), 0);

            controller.OnLanded();

            if (UxEvent != null)
            {
                UxEvent.InvokeMessage(true);
            }

        }
    }

}
