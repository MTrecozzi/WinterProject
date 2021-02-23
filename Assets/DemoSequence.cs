using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSequence : MonoBehaviour, ISequenceData
{



    private Sequence sequence;

    public float duration;

    public TweenObject[] objects;

    public GameObject[] managedObjects;

    private void Start()
    {

    }

    public Sequence GetSequence()
    {

        if (sequence == null)
        {
            InitSequence();
        }

        return sequence;
    }

    public void InitSequence()
    {

        sequence = DOTween.Sequence();

        for (int i = 0; i < managedObjects.Length; i++)
        {
            var x = managedObjects[i].transform.DOMoveY(managedObjects[i].transform.position.y - 10, duration);
            var y = managedObjects[i].transform.DOScale(0, duration);

            sequence.Join(x);
            sequence.Join(y);

        }

        for (int i = 0; i < objects.Length; i++)
        {
            sequence.Join(objects[i].GetSequence());
        }
    }

}
