using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] private float health = 1f;
    [SerializeField] private GameObject destructionEffect;
    
    public void takeDamage(float amount)
    {
        health -= amount;
        if (health < 0f)
        {
            _destroy();
        }
    }

    private void _destroy()
    {
        if(destructionEffect) Instantiate(destructionEffect, transform);
        Destroy(gameObject);
    }
}
