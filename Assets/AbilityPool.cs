using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPool : MonoBehaviour
{

    public int maxCharges;
    public int currentCharges;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public bool IsChargesLeft()
    {
        return currentCharges > 0;
    }

    public void ResetCharges()
    {
        currentCharges = maxCharges;
    }

    public bool TryGetCharge()
    {
        if (currentCharges > 0)
        {
            currentCharges--;
            return true;
        }

        return false;
    }
}
