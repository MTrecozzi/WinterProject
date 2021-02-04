using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePoolingComponent : MonoBehaviour
{
    public CollectibleEventChannel collectibleChannel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            collectibleChannel.Invoke(gameObject.transform);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
