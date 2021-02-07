using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryChannelActivator : MonoBehaviour
{

    public BinaryCrossSceneReference[] references;

    [SerializeField]
    private bool enableOnStart;

    // Start is called before the first frame update
    void Start()
    {

        if (enableOnStart)
        {
            InvokeAllReferences(enableOnStart);
        }
        
    }

    public void InvokeAllReferences(bool param)
    {
        foreach (var reference in references)
        {
            reference.InvokeMessage(param);
        }
    }
}
