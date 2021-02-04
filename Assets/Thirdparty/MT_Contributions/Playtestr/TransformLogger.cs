using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OdinSerializer;
using System.IO;

public class TransformLogger : MonoBehaviour
{

    public Transform transformToLog;

    private List<Vector3> positions;

    public float snapShotInterval;
    private float t;
    private float nextSnapshot;



    // Start is called before the first frame update
    void Awake()
    {
        transformToLog = GameObject.FindGameObjectWithTag("Player").transform;

        positions = new List<Vector3>();

        Debug.Log(Application.persistentDataPath + "/position.sav");
    }

    public void SaveData()
    {
        byte[] bytes = SerializationUtility.SerializeValue(positions, DataFormat.Binary);

        File.WriteAllBytes(Application.persistentDataPath + "/position.sav", bytes);
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Application Quit");

        SaveData();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        t = Time.time;

        if (t >= nextSnapshot)
        {
            nextSnapshot = t + snapShotInterval;

            LogPosition();
        }
    }

    private void LogPosition()
    {
        if (transformToLog == null)
        {
            return;
        }

        positions.Add(transformToLog.position);
    }
}
