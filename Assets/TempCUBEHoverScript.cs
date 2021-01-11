using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class TempCUBEHoverScript : MonoBehaviour, IMoverController
{
    public PhysicsMover Mover;

    Vector3 startPos;

    public float frequency = 1f;

    public float magnitude = 1f;

    public Vector3 rotation = new Vector3(15f, 30f, 45f);

    private void Start()
    {
        startPos = transform.position;
        Mover.MoverController = this;
    }


    // Update is called once per frame
    void Update()
    {
        /*
        Vector3 pos = transform.position;

        pos = startPos + new Vector3(0, Mathf.Sin(Time.time * frequency) * magnitude, 0);

        transform.position = pos;

        Vector3 rot = transform.rotation.eulerAngles;

        rot += rotation; // * Time.deltaTime;

        transform.Rotate(rot * Time.deltaTime);
        */
    }

    public void UpdateMovement(out Vector3 goalPosition, out Quaternion goalRotation, float deltaTime)
    {
        goalPosition = startPos + new Vector3(0, Mathf.Sin(Time.time * frequency) * magnitude, 0);
        
        Vector3 rot = transform.rotation.eulerAngles;

        rot += rotation ;
        goalRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rot), 10f);

    }
}
