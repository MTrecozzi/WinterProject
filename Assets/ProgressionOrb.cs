using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionOrb : MonoBehaviour
{

    public SequenceProgressionManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.NextSequence();
        }
    }
}
