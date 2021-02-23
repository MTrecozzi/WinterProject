using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BounceTrigger))]
public class CrumbleMushroom : MonoBehaviour
{

    public float respawnTime = 3f;

    public float t = 0;

    private BounceTrigger trigger;

    bool disabled;

    public GameObject[] visuals;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<BounceTrigger>();

        trigger.OnPlayerBounced += Crumble;
    }

    private void Crumble()
    {
        trigger.enabled = false;
        ShowVisuals(false);
        t = 0;
        disabled = true;
    }


    public void ShowVisuals(bool show)
    {
        foreach (var x in visuals)
        {
            x.SetActive(show);
        }
    }

    private void Respawn()
    {
        trigger.enabled = true;
        ShowVisuals(true);
        disabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (disabled && t < respawnTime)
        {
            t += Time.deltaTime;

            if (t >= respawnTime)
            {
                t = 0;

                Respawn();
            }
        }
    }
}
