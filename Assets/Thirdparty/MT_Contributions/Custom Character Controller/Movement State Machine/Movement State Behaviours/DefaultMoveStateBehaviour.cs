using KinematicCharacterController;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DashStateBehaviour))]
public class DefaultMoveStateBehaviour : MoveStateBehaviour
{

    [SerializeField]
    public DefaultMoveState defaultMoveState;

    public MTCharacterController controller;

    private DashState dashStateReference;

    private void Awake()
    {
        dashStateReference = GetComponent<DashStateBehaviour>().dashState;

        var transition = new MovementStateTransition(defaultMoveState, CheckDash, dashStateReference);
        transition.destinationHandled = true;

        controller.manager.AddTransition(transition);

        


    }

    public override MovementState[] GetManagedMoveStates()
    {
        return new MovementState[] { defaultMoveState };
    }

    public bool CheckDash()
    {
        bool validTransition = controller.Dash.Buffered && dashStateReference.defaultMoveState.defaultMoveState.dashPool.currentCharges > 0;

        if (validTransition)
        {
            controller.Dash.EatInput();

            Debug.Log("Dash Started");
            dashStateReference.StartDash(dashStateReference.controller.transform.forward);
            dashStateReference.defaultMoveState.defaultMoveState.dashPool.currentCharges--;
        }

        return validTransition;
    }

    private void FixedUpdate()
    {

        for (int i = 0; i < defaultMoveState.passingThroughIgnoredColliders.Count; i++)
        {
            // needs a dynamic algorithm that responds to colliders size
            if (Mathf.Abs((defaultMoveState.passingThroughIgnoredColliders[i].transform.position - controller.transform.position).magnitude) >= 3f)
            {

                Debug.Log("Collider Removed:");

                defaultMoveState.passingThroughIgnoredColliders.RemoveAt(i);
            }
        }

    }



    public void IncrementTempJumps()
    {
        defaultMoveState.jumpPool.currentCharges = defaultMoveState.jumpPool.maxCharges + 1;
    }

    public void ResetAbilities() => defaultMoveState.ResetAbilities();


    public void DampenAirAccel() => defaultMoveState.DampenAirAccel();

    public KinematicCharacterMotor Motor => defaultMoveState.Motor;


}
