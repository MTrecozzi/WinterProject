using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TweenType
{
    Move, Scale
}

public class TweenObject : MonoBehaviour, ISequenceData
{
    public TweenType type;

    public Transform endValue;

    private Sequence sequence;
    public float duration;

    public Sequence GetSequence()
    {
        if (sequence == null)
        {
            InitSequence();
        }

        return sequence;
    }

    private void InitSequence()
    {
        sequence = DOTween.Sequence();

        Tween x = type switch
        {
            TweenType.Move => transform.DOMove(endValue.position, duration),
            _ => throw new NotImplementedException(),
        };
    }

    public void PlayForward()
    {
        sequence.PlayForward();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
