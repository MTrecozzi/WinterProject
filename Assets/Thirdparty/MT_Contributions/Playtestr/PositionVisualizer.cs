using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OdinSerializer;
using System.IO;
using System;

public class PositionVisualizer : MonoBehaviour
{
    public GameObject vis;

    private Transform visTransform;

    public float interval = 0.25f;

    public List<Vector3> positions;

    string filePath;

    private int i = 0;

    private float t;
    private float nextTime;

    // Start is called before the first frame update
    void Awake()
    {
        filePath = Application.persistentDataPath + "/position.sav";

        if (this.enabled)
        {
            visTransform = GameObject.Instantiate(vis).transform;
        }

        LoadState();

    }

    private void Start()
    {
       visTransform.position = positions[i];
    }

    public void LoadState()
    {
        if (!File.Exists(filePath)) return; // No state to load

        byte[] bytes = File.ReadAllBytes(filePath);
        this.positions = SerializationUtility.DeserializeValue<List<Vector3>>(bytes, DataFormat.Binary);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        t = Time.time;

        if (t > nextTime)
        {

            nextTime = t + interval;

            i++;


        }

        VisSnapShot();
    }

    private void VisSnapShot()
    {
        if (positions == null) { return; }

        if (i + 1 >= positions.Count) { return; }

        var newPosition = Vector3.Lerp(positions[i], positions[i + 1], 1 - (nextTime - t) / interval);

        visTransform.position = newPosition;
    }
}
