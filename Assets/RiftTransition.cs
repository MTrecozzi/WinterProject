using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RiftTransition : MonoBehaviour
{

    Sequence mySequence;

    public float duration;

    // Start is called before the first frame update
    void Start()
    {
        mySequence = DOTween.Sequence();

        mySequence.Pause();
        mySequence.SetAutoKill(false);

        mySequence.Join(transform.DOScale(0, duration));
        mySequence.Join(transform.DOMoveY(transform.position.y - 10f, duration));

        mySequence.Complete();
    }

    public void Play()
    {
        mySequence.PlayForward();
    }

    public void Rewind()
    {
        mySequence.SmoothRewind();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
