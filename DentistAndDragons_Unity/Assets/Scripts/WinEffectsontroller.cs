using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinEffectsontroller : MonoBehaviour
{
    public GameObject effect;
    public float size=10f;
    void Start()
    {
        InvokeRepeating("spawnEffect",0f,0.1f);
    }

    void spawnEffect()
    {
        Vector3 rand = new Vector3(Random.Range(0f, size),Random.Range(0f, size),Random.Range(0f, size));
        Instantiate(effect,transform.position + rand,Quaternion.identity);
    }
}
