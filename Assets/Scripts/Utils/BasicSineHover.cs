using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSineHover : MonoBehaviour
{
    public float frequency = 2.5f;
    public float magnitude = 0.4f;

    Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        transform.position = startPos + new Vector3(0, Mathf.Sin(Time.time * frequency) * magnitude, 0);
    }





}
