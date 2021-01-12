using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCubeComponent : MonoBehaviour
{
    public GameEventListener cubeEventListener;
    public GameObject Cube;

    private void Start()
    {
        cubeEventListener = gameObject.GetComponent<GameEventListener>();
        
    }
    private void OnEnable()
    {
        cubeEventListener.Response += ActivateCube();
    }
    void ActivateCube()
    {
        Cube.SetActive(true);
    }




}
