using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpThisCUBE : MonoBehaviour
{
    public GameObject fist;
    public GameObject displayFist;

    public float radius = 1f;

    public string tagToSearch = "Player";
    public LayerMask hit;

    private void Start()
    {
    }


    private void FixedUpdate()
    {
        var colls = Physics.OverlapSphere(transform.position, radius, hit);

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].gameObject.tag.Equals(tagToSearch))
            {
                fist.SetActive(true);
                displayFist.SetActive(false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }




}
