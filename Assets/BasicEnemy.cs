using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, IHandleFistStuff
{

    public Color colorA;
    public Color colorB;

    [Range(0,50)]
    public float health = 20;
    [Range(0,50)]
    public float maxHealth = 20;


    public MeshRenderer meshRenderer;

    private void Start()
    {
        health = maxHealth;
        if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Grab()
    {

    }

    public void Push(Vector3 Direction)
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0) health = 0f;
    }

    private void Update()
    {

        float percent = health / maxHealth;

        meshRenderer.material.color = Color.Lerp(colorB, colorA, percent);
    }

}
