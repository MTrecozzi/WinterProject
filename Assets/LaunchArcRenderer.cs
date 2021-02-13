using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaunchArcRenderer : MonoBehaviour
{
    LineRenderer lr;

    public BounceTrigger launchSource;

    public Vector3 velocity;

    public float gravity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
