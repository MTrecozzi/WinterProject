using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedAbilityPool : MonoBehaviour
{

    public int maxCharges;
    public int currentCharges;

    public int chargesPerSprite = 2;
    private int currentSprites;

    public BinaryCrossSceneReference abilityPickUP;


    // Start is called before the first frame update
    void Start()
    {
        

        abilityPickUP.BinaryMessage += GainSprite;
    }

    private void GainSprite(bool obj)
    {
        currentSprites++;

        if (currentSprites == chargesPerSprite)
        {
            currentSprites = 0;

            maxCharges++;
        }
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
