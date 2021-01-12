using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;


public class CubeScript : MonoBehaviour, IMoverController, IHandleFistStuff
{

    Vector3 velocity;

    public void Grab()
    {
    }

    public void Push(Vector3 Direction)
    {
        velocity = (transform.position - Direction).normalized;
    }

    public void TakeDamage(float damage)
    {
    }

    public void UpdateMovement(out Vector3 goalPosition, out Quaternion goalRotation, float deltaTime)
    {
            
        goalPosition = transform.position + velocity;
        goalRotation = Quaternion.identity;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

