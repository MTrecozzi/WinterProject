using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryDestructableComponent : MonoBehaviour, IDestructable
{

    public event Action OnDestroy;

    public void Destroy()
    {
        gameObject.SetActive(false);
        OnDestroy?.Invoke();
    }
}
