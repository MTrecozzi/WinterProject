using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisterDamage : MonoBehaviour
{

    public float damage;
    public float pushForce = 10f;

    public Transform playerPos;

    public LayerMask Punchable;

    public BoxCollider thisCol;

    public string tagForSearching;

    private void Start()
    {
        if(thisCol == null )
        {
            thisCol = this.gameObject.GetComponent<BoxCollider>();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        var fistStuff = other.gameObject.GetComponent<IHandleFistStuff>();

        if(fistStuff != null)
        {
            fistStuff.Push(transform.position);
            fistStuff.TakeDamage(damage);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        var fistStuff = collision.gameObject.GetComponent<IHandleFistStuff>();
        if(fistStuff != null)
        {
            fistStuff.Push((collision.gameObject.transform.position - transform.position).normalized * pushForce);
            fistStuff.TakeDamage(damage);

        }


    }









}
