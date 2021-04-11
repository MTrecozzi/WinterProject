using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenStateBehaviour : MoveStateBehaviour
{
    [SerializeField]
    public ScreenState screenState;

    public LayerMask screenLayers;

    public GameObject playerMesh;

    public GameObject emojiMesh;

    public SkinnedMeshRenderer[] meshes;

    public override MovementState[] GetManagedMoveStates()
    {
        return new MovementState[] { screenState };
    }

    // Start is called before the first frame update
    void Start()
    {

        MovementStateTransition LeftCollision = new MovementStateTransition(screenState, ExitCollision, screenState.controller.manager.defaultMoveStateBehaviour.defaultMoveState);

        screenState.controller.manager.AddTransition(LeftCollision);

        screenState.StateEnterOrExit += HandleUX;

        meshes = playerMesh.GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    private void HandleUX(bool entering, Vector3 normal)
    {
        if (entering)
        {
            // disable mesh

            foreach (var mesh in meshes)
            {
                mesh.enabled = false;
            }

            emojiMesh.SetActive(true);

            emojiMesh.transform.rotation = Quaternion.LookRotation(screenState.wallNormal, Vector3.up);
        }

        if (!entering)
        {
            foreach (var mesh in meshes)
            {
                mesh.enabled = true;
            }

            emojiMesh.SetActive(false);
        }
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

            screenState.controller.manager.Motor.BaseVelocity += screenState.wallNormal * 10;
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

    public float baseSpeed = 15f;

    private float _speed;

    public event Action<bool, Vector3> StateEnterOrExit;

    private bool normalized = false;

    public void SetUp(Vector3 _wallNormal, bool _normalized = false, float pSpeed = 0f)
    {

        if (pSpeed != 0f)
        {
            _speed = pSpeed;
        }

        else
        {
            _speed = baseSpeed;
        }

        Debug.Log("HIT NORMAL OF SCREEN STATE: " + wallNormal);

        wallNormal = _wallNormal;
        normalized = _normalized;

    }

    public override void Initialize()
    {
        base.Initialize();

        StateEnterOrExit?.Invoke(true, wallNormal);
    }

    public override void CleanUp()
    {
        StateEnterOrExit?.Invoke(false, wallNormal);

        controller.manager.defaultMoveStateBehaviour.defaultMoveState.ResetAbilities();
    }

    public override void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        return;
    }

    public override void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        // This is the normalized Move Input
        var rawInput = controller.controls.Standard.ControlStick.ReadValue<Vector2>();

        // when coming at a side angle, the relative vector is messed up.

        Vector3 vec3Input = new Vector3(rawInput.x, rawInput.y, 0);

        Vector3 relative = Camera.main.transform.TransformDirection(vec3Input);

        // Could probably Make Relativet to surface with surface normal right.input * surface Normal up.input [ !!!!!!!!! ]

        // var simple vector = ....

       // Debug.Log("RELATIVE VECTOR: " + relative);

        var projectedVector = Vector3.ProjectOnPlane(relative, wallNormal);

        // CURRENTLY NOT RUNNING NORMALIZED
        var normalizedVector = projectedVector.normalized;

        currentVelocity = (normalized ? normalizedVector : projectedVector) * _speed;
    }

}
