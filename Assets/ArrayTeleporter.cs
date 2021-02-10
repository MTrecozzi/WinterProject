using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayTeleporter : MonoBehaviour
{

    [Header("Input Controls")]
    public KeyCode NextPosition = KeyCode.N;
    public KeyCode PreviousPosition = KeyCode.B;
    public KeyCode LogCurrentGameObjectPosition = KeyCode.G;

    public GameObject[] positions;
    public MTCharacterController controller;

    

    public int index = 0;

    private void Awake()
    {
        controller = FindObjectOfType<MTCharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        controller.Motor.SetPosition(positions[index].transform.position);

    }

    public void Teleport(int next)
    {
        index += next;

        if (index >= positions.Length)
        {
            index = 0;
        }

        if (index < 0)
        {
            index = positions.Length - 1;
        }

        controller.Motor.SetPosition(positions[index].transform.position);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Teleport(1);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Teleport(-1);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Current GameObject Index: " + positions[index].name);
        }
    }
}
