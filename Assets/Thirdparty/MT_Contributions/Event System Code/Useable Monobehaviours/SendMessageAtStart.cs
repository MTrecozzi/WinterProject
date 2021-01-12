using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMessageAtStart : MonoBehaviour
{
    [SerializeField]
    private bool sendValue = true;

    public BinaryCrossSceneReference crossSceneReference;

    // Start is called before the first frame update
    void Start()
    {
        crossSceneReference.InvokeMessage(sendValue);
    }
}
