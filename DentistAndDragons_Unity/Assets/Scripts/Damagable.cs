using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] private float health = 1f;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject destructionEffect;
    
    public void takeDamage(float amount, Vector3 hitPosition , Vector3 hitNormal)
    {
        health -= amount;
        
        if (hitEffect) Instantiate(hitEffect, hitPosition,Quaternion.FromToRotation(transform.up, hitNormal));
        
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
