using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBeam : MonoBehaviour
{

    public RaycastSystem raycaster;

    public BinaryCrossSceneReference ChargeBeamActivatedSO;

    // Start is called before the first frame update
    void Start()
    {
        ChargeBeamActivatedSO.BinaryMessage += ActivateAbility;
    }

    private void ActivateAbility(bool obj)
    {
        this.enabled = obj;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // should be able to pass custom physics layer as a parameter
        var hit = raycaster.CastRayFromScreen(100);

        var destructable = hit.transform.GetComponent<IDestructable>();

        if (destructable != null)
        {
            destructable.Destroy();
        }

    }
}
