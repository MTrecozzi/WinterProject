using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryAbilityPoolSetter : MonoBehaviour
{

    public BinaryCrossSceneReference abilityReference;
    public AbilityPool abilityPool;
    public int setValue = 1;

    // Start is called before the first frame update
    void Awake()
    {
        abilityReference.BinaryMessage += HandleAbilityPickUp;
    }

    private void HandleAbilityPickUp(bool newValue)
    {
        if (newValue)
        {
            abilityPool.maxCharges = setValue;
            abilityPool.currentCharges = setValue;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
