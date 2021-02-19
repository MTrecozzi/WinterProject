using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SequenceProgressionManager : MonoBehaviour
{
    public Transform progressionOrb;

    public Transform secondPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void NextSequence()
    {
        progressionOrb.transform.DOMove(secondPosition.position, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
