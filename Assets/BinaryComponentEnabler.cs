using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryComponentEnabler : MonoBehaviour
{

    public BinaryCrossSceneReference abilityChannel;

    public Component abilityComponent;

    // Start is called before the first frame update
    void Awake()
    {
        abilityChannel.BinaryMessage += HandleAbilityMessage;
    }

    private void HandleAbilityMessage(bool obj)
    {
        // abilityComponent.enabled = obj;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
