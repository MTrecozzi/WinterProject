using KinematicCharacterController;
using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpVelocityModifer : MonoBehaviour
{

    public Rigidbody rb;
    public ExampleCharacterController character;

    public KinematicCharacterMotor motor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        

        // simple responsive jump algorithm.

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (rb.velocity.y > 0)
            {
                Vector3 newVelocity = rb.velocity;

                newVelocity.y = newVelocity.y / 2;
                
                character.UpdateVelocity(ref newVelocity, Time.deltaTime);
            }
        }
    }
}
