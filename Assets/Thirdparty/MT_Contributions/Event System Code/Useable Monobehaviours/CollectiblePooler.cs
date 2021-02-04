using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePooler : MonoBehaviour
{
    public CollectibleEventChannel targetChannel;

    public int numberCollected = 0;

    // Start is called before the first frame update
    void Start()
    {
        targetChannel.CollectibleEvent += PoolCollectible;
    }

    private void PoolCollectible(Transform obj)
    {
        obj.gameObject.SetActive(false);

        numberCollected++;
    }


}
