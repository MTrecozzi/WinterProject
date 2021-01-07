using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistInteractable : MonoBehaviour, IHandleFistStuff
{
    public float pushResistance = 1f; // none for now

    private MeshRenderer renddd;

    private float time;
    
    private bool moving;
    private Vector3 movePosition;

    private void Start()
    {
        renddd = GetComponent<MeshRenderer>();
        time = 0f;
    }

    public void Grab()
    {
    }

    public void Push(Vector3 direction)
    {
        direction.y = Mathf.Clamp(direction.y, 1, 10);

        movePosition = direction * 2f;
        moving = true;

    }

    public void TakeDamage(float damage)
    {
        renddd.material.color = Color.red;


    }

    private void Update()
    {
        time += Time.deltaTime;

        if(time > 1f)
        {
            renddd.material.color = Color.green;
            time = 0f;
        }

        if(moving)
        {

            transform.position = Vector3.Lerp(transform.position, movePosition, 2f);



        }
        if(Vector3.Distance(transform.position, movePosition) <= 0.1)
        {

            moving = false;

            movePosition = Vector3.zero;


        }

    }

}
