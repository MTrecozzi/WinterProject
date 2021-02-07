using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBinaryReferencesOnTriggerEnter : MonoBehaviour
{

    public BinaryChannelActivator activator;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            activator.InvokeAllReferences(true);
            gameObject.SetActive(false);
        }

    }
}
