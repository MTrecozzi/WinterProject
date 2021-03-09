using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAimSetter : MonoBehaviour
{
    public MultiAimConstraint constraint;
    public RigBuilder builder;
    public Transform aimTransform;

    [SerializeField]
    private Transform playerTransform;

    private Vector3 defaultPos;

    private WeightedTransform weightedTransform;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = aimTransform.position;
    }

    private void Update()
    {
        var targePos = (playerTransform == null) ? defaultPos : playerTransform.position;

        aimTransform.position = Vector3.Lerp(aimTransform.position, targePos, 2 * Time.deltaTime);

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
        }
    }



    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (constraint.data.sourceObjects.Count > 0)
            {
                playerTransform = null;
            }
        }
    }
}
