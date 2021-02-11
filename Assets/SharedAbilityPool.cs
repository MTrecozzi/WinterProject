using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedAbilityPool : MonoBehaviour
{

    public int maxCharges;
    public int currentCharges;

    // Start is called before the first frame update
    void Start()
    {
        currentCharges = maxCharges;
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
