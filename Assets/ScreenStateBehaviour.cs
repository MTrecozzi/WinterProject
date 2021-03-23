using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenStateBehaviour : MoveStateBehaviour
{
    [SerializeField]
    public ScreenState screenState;

    public LayerMask screenLayers;

    public override MovementState[] GetManagedMoveStates()
    {
        return new MovementState[] { screenState };
    }

    // Start is called before the first frame update
    void Start()
    {

        MovementStateTransition LeftCollision = new MovementStateTransition(screenState, ExitCollision, screenState.controller.manager.defaultMoveStateBehaviour.defaultMoveState);

        screenState.controller.manager.AddTransition(LeftCollision);
    }

    private bool ExitCollision()
    {
        RaycastHit closestHit;

        RaycastHit[] hits = new RaycastHit[4];

        int numbHits = screenState.controller.manager.Motor.CharacterCollisionsRaycast(transform.position, -screenState.wallNormal, 1f, out closestHit, hits, false);

        int screensCollided = 0;

        for (int i = 0; i < numbHits; i++)
        {
            if (((screenLayers.value & (1 << hits[i].transform.gameObject.layer)) > 0))
            {
                screensCollided++;
            }
        }


        Debug.Log("HITS: " + screensCollided);

        bool validTransition = screensCollided <= 0 || screenState.controller.Jump.Buffered;

        if (validTransition && screenState.controller.Jump.Buffered)
        {
            screenState.controller.Jump.EatInput();
            Debug.LogWarning("Should Use a Velocity State Instead of Crappy Jump Force");
            screenState.controller.manager.Motor.BaseVelocity.y = 15;
        }

        
        

        return validTransition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class ScreenState : MovementState
{
    public Vector3 wallNormal;

    public float speed = 15f;

    public void SetUp(Vector3 _wallNormal)
    {

        Debug.Log("HIT NORMAL OF SCREEN STATE: " + wallNormal);

        wallNormal = _wallNormal;
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        // This is the normalized Move Input
        var rawInput = controller.controls.Standard.ControlStick.ReadValue<Vector2>();

        // when coming at a side angle, the relative vector is messed up.

        Vector3 vec3Input = new Vector3(rawInput.x, rawInput.y, 0);

        Vector3 relative = Camera.main.transform.InverseTransformDirection(vec3Input);

       // Debug.Log("RELATIVE VECTOR: " + relative);

        var projectedVector = Vector3.ProjectOnPlane(relative, wallNormal);

        // CURRENTLY NOT RUNNING NORMALIZED
        var normalizedVector = projectedVector.normalized;

        currentVelocity = projectedVector * speed;
    }

}
