using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameObjectActivationReciever : MonoBehaviour
{
    public BinaryCrossSceneReference reference;
    public GameObject Cube;

    private void Awake()
    {
        reference.BinaryMessage += ActivateCube;
    }

    private void ActivateCube(bool value)
    {
        Cube.SetActive(value);
    }




}
